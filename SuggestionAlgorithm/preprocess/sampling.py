import utility as ut
import random
from preprocess import pre_calculate
from collections import OrderedDict
from operator import itemgetter
from sklearn.externals import joblib
import networkx as nx

# source, sink,
# source_indegree, source_outdegree,
# sink_indegree, sink_outdegree,
# shared_follower_weighted,
# round2_shared_follower_weighted


HEADER = ["source",                 # 0
          "sink",                   # 1
          "source_indegree",        # 2
          "source_indegree_level",  # 3
          "source_outdegree",       # 4
          "source_outdegree_level", # 5
          "sink_indegree",          # 6
          "sink_indegree_level",    # 7
          "sink_outdegree",         # 8
          "sink_outdegree_level",   # 9
          "weighted_in_share_friends",# 10
          "weighted_out_share_friends",# 11
          "source_indegree_order",  # 12
          "source_outdegree_order", # 13
          "sink_indegree_order",    # 14
          "sink_indegree_order",    # 15
          "is_follow"]              # 16


def make_sample(max_node_count: int, sink_node_count:int,
                max_outdegree:int,
                sink_dict_file,
                source_dict_file,
                test_file = ut.ORIGINAL_TEST_FILE,
                sample_pair_file_with_features = ut.SAMPLE_PAIR_WITH_FEATURE_FILE,
                test_pair_file_with_features = ut.TEST_FILE_WITH_FEATURES,
                use_networkx = False
                ):

    # begin to do samping
    ut.log("begin to make sample...")

    sink_dict = ut.read_as_dict(sink_dict_file)
    sink_dict_keys_set = set(sink_dict.keys())
    sink_dict = OrderedDict(sorted(sink_dict.items(), key=itemgetter(1), reverse=True))


    source_dict = ut.read_as_dict(source_dict_file)
    source_dict_keys_set = set(source_dict.keys())


    testing_source_ids,test_sink_ids,test_pair = ut.get_test_file_data(test_file)

    ut.log("random select more start node...")
    random_nodes = _random_start_point(source_dict,testing_source_ids, max_node_count - len(testing_source_ids),max_outdegree)
    start_points = testing_source_ids.union(random_nodes)

    ut.log("random select more sink node...")
    random_sink_nodes = _random_sink_point(sink_dict, test_sink_ids, sink_node_count - len(test_sink_ids),
                                      1, 2000)
    end_points = test_sink_ids.union(random_sink_nodes)

    # graph
    ut.log("make positive pairs...")
    positive_pairs = []
    finished_source_node_list = list()
    positive_nodes_set = set()
    for source_node in start_points:
        if not source_node in source_dict_keys_set:
            break

        positive_nodes_set.add(source_node)

        for sink_node in source_dict[source_node]:
            if len(source_dict[source_node]) > max_outdegree:
                if sink_node in end_points:
                    positive_pairs.append((source_node,sink_node))
                    positive_nodes_set.add(sink_node)
            else:
                if sink_node in end_points:
                    positive_pairs.append((source_node, sink_node))
                    positive_nodes_set.add(sink_node)

        finished_source_node_list.append(source_node)
        ut.log("{}/{}".format(len(finished_source_node_list),max_node_count))

    # create calculator to calculate features
    calc = pre_calculate.calculator(source_dict, source_dict_keys_set, sink_dict,sink_dict_keys_set)

    positive_count = 0
    # saved_to = sample_pair_file_with_features+"{}.{}.{}.csv".format(max_node_count,sink_node_count,max_outdegree)
    # positive pairs
    ut.log("calculate positive pair features and save to file {}...".format(sample_pair_file_with_features + "{}.csv".format(max_node_count)))
    # ut.write_list_csv(.format(max_node_count), sample_source_pair_list)
    with open(sample_pair_file_with_features+"{}.csv".format(max_node_count), 'w')as f:
        f.write("{}\n".format(",".join(HEADER)))

        featured_pairs = list()

        for pair in positive_pairs:
            start = pair[0]
            end = pair[1]
            if positive_count % 1000 == 0 :
                ut.log("{}/{}".format(positive_count, len(positive_pairs)))

            this_pair = calc._calculate_pair_features(start, end, True)

            # sample_source_pair_list.append(this_pair)
            featured_pairs.append(this_pair)
            row_text = ",".join(map(str,this_pair))
            f.write("{}\n".format(row_text))
            positive_count += 1

        ut.log('\npositive pair completed')

        # # write file into graph DSG format
        # _write_graph_file_DGS(featured_pairs, positive_nodes_set, sample_pair_file_with_features, max_node_count)

        # negative pairs
        # 0. get all sinks
        ut.log("generate negative pair features...")
        original_sink_list = list(end_points)
        sink_count = len(end_points)
        start_points_list = list(start_points)
        source_count = len(start_points)
        # positive_count = len(sample_source_pair_list)
        negative_count = 0
        negative_count_target = positive_count #int(35 * positive_count / 65)

        while negative_count < negative_count_target:
            source_idx = random.randint(0,source_count-1)
            nga_source = start_points_list[source_idx]

            sink_idx = random.randint(0, sink_count-1)
            nga_sink = original_sink_list[sink_idx]

            if nga_source not in source_dict:

                break

            if nga_sink not in source_dict[nga_source]:
                this_pair = calc._calculate_pair_features(nga_source,nga_sink,False)

                row_text = map(str, this_pair)
                f.write("{}\n".format(",".join(row_text)))
                # sample_source_pair_list.append(this_pair)
                negative_count += 1

                if negative_count % 1000 == 0:
                    ut.log("{}/{}".format(negative_count, negative_count_target))
                # if negative_count % 100000 == 0:
                #     ut.log('')

            # ut.log("{}/{}".format(negative_count, positive_count))

        ut.log('\nnegative pair completed')


        # out of package
        ut.log("generate out of package pair features...")
        # original_sink_list = list(end_points)
        sink_count = len(end_points)
        start_points_list = list(start_points)

        # positive_count = len(sample_source_pair_list)
        out_of_package_count = 0
        out_of_package_count_target =  positive_count
        original_train_pair = ut.read_as_list(ut.ORIGINAL_TRAIN_FILE,"\t")
        source_count = len(original_train_pair)


        while out_of_package_count < out_of_package_count_target:


            row_idx = random.randint(0, source_count - 1)
            row = original_train_pair[row_idx]
            if len(row) < 3:
                continue
            sink_idx = random.randint(1, len(row) - 1)

            source = row[0]
            sink = row[sink_idx]
            this_pair = calc._calculate_pair_features(source, sink, True)
            row_text = map(str, this_pair)
            f.write("{}\n".format(",".join(row_text)))
            out_of_package_count += 1
            if out_of_package_count % 1000 == 0:
                ut.log("{}/{}".format(out_of_package_count, out_of_package_count_target))


        ut.log('\nout of package pair completed')

        # ut.write_list_csv(sample_pair_file_with_features+"{}.csv".format(max_node_count), sample_source_pair_list)


    # test public features
    featured_test_pair_list = list()
    featured_test_pair_list.append(HEADER)
    for tp in test_pair:
        start = tp[0]
        end = tp[1]
        this_pair = calc._calculate_pair_features(start,end,False)
        featured_test_pair_list.append(this_pair)

    ut.write_list_csv(test_pair_file_with_features+"{}.csv".format(max_node_count),featured_test_pair_list)

    if use_networkx:
        apply_networkx(max_node_count,testing_source_ids.union(test_sink_ids))


def _random_start_point(source_dict,existing_points, n_sample, max_outdegree):
    sample_nodes = set()
    source_count = len(source_dict)
    source_list = list(source_dict.keys())
    loop_count = 0
    while loop_count < n_sample:
        idx = random.randint(0, source_count-1)
        node = source_list[idx]
        if node not in existing_points and len(source_dict[node]) <= max_outdegree:
            sample_nodes.add(node)
        loop_count += 1

    return sample_nodes


def _random_sink_point(sink_dict, existing_points, n_sample, min_indegree = 2, max_mindegree = 2000):
    sample_nodes = set()
    for node, follower_list in sink_dict.items():
        indegree = len(follower_list)
        if node not in existing_points and indegree <= max_mindegree and indegree >= min_indegree:
            sample_nodes.add(node)
        if len(sample_nodes) >= n_sample:
            break

    # while len(sample_nodes) < n_sample:
    #     idx = random.randint(0, sink_count - 1)
    #     node = sink_list[idx]
    #     indegree = len(sink_dict[node])
    #     if node not in existing_points and indegree <= max_mindegree and indegree >= min_indegree:
    #         sample_nodes.add(node)

    return sample_nodes
#
#
#
# def _write_graph_file_DGS(positive_pairs,nodes_set, sample_pair_file_with_features,max_node_count):
#
#     with open(sample_pair_file_with_features + "{}.csv.dgs".format(max_node_count), 'w')as javaG:
#
#         javaG.write('DGS004\n')
#         javaG.write("null 0 0\n")
#
#         for node in nodes_set:
#             javaG.write("an {}\n".format(node))
#
#         i = 1
#         for edge in positive_pairs:
#             javaG.write("ae {} {} > {} sink_indegree:{} \n".format(i,edge[0],edge[1],edge[5]))
#             i += 1

def apply_networkx(node_count, test_nodes):
    columns = [0,   #0 source
               1,   #1 sink
               4,   #2 source outdegree
               6,   #3 sink indegree
               10,  #4 weighted_in_share_friends
               11,  #5 weighted_out_share_friends
               16]  #6 flag
    original_columns_titles = [HEADER[i] for i in columns]

    sample_data = ut.read_as_list(ut.DATA_DIR + "sample.with_feature.{}.csv".format(node_count))[1:]
    test_data = ut.read_as_list(ut.DATA_DIR + "test-public_with_features.{}.csv".format(node_count))[1:]

    column_filter = lambda x: [x[i] for i in columns]
    sample_data = list(map(column_filter, sample_data))
    test_data = list(map(column_filter, test_data))

    DG = nx.Graph()
    DG.add_nodes_from(test_nodes)

    trans_func = lambda x: [x[0], x[1], 1 / int(x[3])]

    weighted_edges = list()
    negative_nodes_set = set()

    for pair in sample_data:
        if pair[-1] == '1':
            weighted_edges.append(trans_func(pair))
        else:
            negative_nodes_set.add(pair[0])
            negative_nodes_set.add(pair[1])

    # list(map(trans_func, sample_data))

    DG.add_weighted_edges_from(weighted_edges,weight='weight')
    DG.add_nodes_from(negative_nodes_set)

    ut.log("calculating networkx features for train data...")
    feature_calculate(DG,sample_data,original_columns_titles,ut.DATA_DIR + "sample.with_feature.{}.networx.csv".format(node_count))

    ut.log("calculating networkx features for test data...")
    feature_calculate(DG,test_data,original_columns_titles,ut.DATA_DIR+"test-public_with_features.{}.networx.csv".format(node_count))

    # # Sample data calculation
    # # TODO Resource allocation index is for undirected graph
    # pairs = list(map(lambda x: (x[0], x[1]), sample_data))
    # jaccard = nx.jaccard_coefficient(DG, pairs)
    # preferential = nx.preferential_attachment(DG, pairs)
    # rai = nx.resource_allocation_index(DG, pairs)
    #
    # # shortest path
    # total = len(sample_data)
    # current = 0
    # for row_data in zip(sample_data,jaccard,preferential,rai):
    #     row = row_data[0]
    #     thisjaccard = row_data[1][2]
    #     thispreferential = row_data[2][2]
    #     thisrai = row_data[3][2]
    #
    #     # pred = row_data[1]
    #     # resource_allocation_index = pred[2]
    #     ut.log("calculating {}/{}...".format(current,total))
    #     path_length = 99999
    #     try:
    #         path = nx.shortest_path(DG, row[0], row[1], 'weight')
    #         path_length = len(path)
    #     except:
    #         pass
    #
    #     # shortest path
    #     row.insert(-1,path_length)
    #
    #     # jaccard
    #     row.insert(-1,thisjaccard)
    #
    #     # preferential
    #     row.insert(-1, thispreferential)
    #
    #     # rai
    #     row.insert(-1,thisrai)
    #
    #     current += 1
    #
    #     #RAI
    #     # row.insert(-1,resource_allocation_index)
    # original_columns_titles.insert(-1,"shortest_path_count")
    # original_columns_titles.insert(-1, "jaccard")
    # original_columns_titles.insert(-1, "preferential")
    # original_columns_titles.insert(-1, "rai")
    #
    #
    # sample_data.insert(0,original_columns_titles)
    #
    # ut.write_list_csv(ut.DATA_DIR + "sample.with_feature.{}.networx.csv".format(node_count),sample_data)
    #
    # return sample_data

def feature_calculate(g,data, column_names,save_to):
    pairs = list(map(lambda x: (x[0], x[1]), data))
    jaccard = nx.jaccard_coefficient(g, pairs)
    preferential = nx.preferential_attachment(g, pairs)
    rai = nx.resource_allocation_index(g, pairs)

    # shortest path
    total = len(data)
    current = 0
    for row_data in zip(data, jaccard, preferential, rai):
        row = row_data[0]
        try:
            thisjaccard = row_data[1][2]
        except:
            thisjaccard = -1

        try:
            thispreferential = row_data[2][2]
        except:
            thispreferential = -1

        try:
            thisrai = row_data[3][2]
        except:
            thisrai = -1

        # pred = row_data[1]
        # resource_allocation_index = pred[2]
        if current % 1000 == 0:
            ut.log("calculating {}/{}...".format(current, total))
        path_length = 99999
        try:
            path = nx.shortest_path(g, row[0], row[1], 'weight')
            path_length = len(path)
        except:
            pass

        # shortest path
        row.insert(-1, path_length)

        # jaccard
        row.insert(-1, thisjaccard)

        # preferential
        row.insert(-1, thispreferential)

        # rai
        row.insert(-1, thisrai)

        current += 1

    original_columns_titles = list(column_names)
    original_columns_titles.insert(-1, "shortest_path_count")
    original_columns_titles.insert(-1, "jaccard")
    original_columns_titles.insert(-1, "preferential")
    original_columns_titles.insert(-1, "rai")

    data.insert(0, original_columns_titles)

    ut.write_list_csv(save_to, data)

