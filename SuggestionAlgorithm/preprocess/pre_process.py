# -1. generate outdegree dict
# 0. clean data and gnenerate cleaned dict
# 1. sampling and generate sample dict
# 2. generate source node feature dict
# 3. generate sample pair feature dict
# 4. generate test node and pair features
# 5. write to file
import utility as ut
from collections import OrderedDict
from operator import itemgetter
from sklearn import utils as skut

OUTDEGREE_RANGE = [0, 100, 1000, 10000, 100000, 1000000]
OUTDEGREE_RANGE_TITLES = [
    "0-100",
    "100-1000",
    "1000-10000",
    "10000-100000",
    "100000-1000000",
    "1000000+"
]


def get_range(count):
    for i in range(len(OUTDEGREE_RANGE)-1):
        if count >= OUTDEGREE_RANGE[i] and count< OUTDEGREE_RANGE[i+1]:
            return i
    return len(OUTDEGREE_RANGE) - 1



def outdegree_dist(from_file,save_to):
    """
    calculate the outdegree of all source nodes and save it into a file with csv format
    :param from_file:
    :param save_to:
    :return:
    """
    ut.log("begin to clean source modelling data in file {}...".format(from_file))

    single_dist = dict()
    single_dist_sources = dict()

    with open(from_file, "r") as f:
        for line in f:
            l = line.replace('\n', '')
            data = l.split('\t')
            outdegree = len(data) - 1
            single_dist[outdegree] = ut._increaseDict(single_dist,outdegree)
            if outdegree in single_dist_sources:
                single_dist_sources[outdegree].append(data[0])
            else:
                single_dist_sources[outdegree] = [data[0]]

    dist = dict()
    dist_sources = dict()
    for i in range(len(OUTDEGREE_RANGE)):
        dist[i] = 0
        dist_sources[i] = []

    for key,value in single_dist.items():
        assert isinstance(value, int)
        dist[get_range(key)] = dist[get_range(key)] + value

    sorted_dist = OrderedDict(sorted(dist.items(), key=itemgetter(0), reverse=True))
    print("Distribution of outdegree for sources:")
    print(sorted_dist)

    for key,value in single_dist_sources.items():
        dist_sources[get_range(key)].extend(value)

    with open(save_to, 'w') as f:
        for key,value in dist_sources.items():
            f.write("{outdegree},{count}\n".format(outdegree=OUTDEGREE_RANGE_TITLES[key],count=','.join(value)))


    ################################################################
    ####   Uncomment below to plot a figure for distribution    ####
    ################################################################
    # import matplotlib
    # matplotlib.use('TkAgg')
    # import matplotlib.pyplot as plt
    # x_titles = OUTDEGREE_RANGE_TITLES
    # x_titles.reverse()
    # plt.bar(list(sorted_dist.keys()),
    #         list(sorted_dist.values()),
    #         tick_label=x_titles,
    #         alpha=0.9, width=0.35, color='lightskyblue')
    #
    # plt.xlabel("X-axis")
    # plt.ylabel("Y-axis")
    # plt.title("bar chart")
    #
    # plt.tight_layout()
    # # plt.show()
    # plt.savefig(data_dir + "source_follow_count_dist.png")
    # # return sorted_dist

    # return dist_sources



def clean_source(min_out_degree,
                 max_out_degree,
                 from_file=ut.ORIGINAL_TRAIN_FILE,
                 save_to = ut.CLEARNED_TRAIN_FILE,
                 test_file = ut.ORIGINAL_TEST_FILE):

    ut.log("begin to clean source modelling data in file {}...".format(from_file))
    ut.log("with min outdegree={}, max_outdegree={}".format(min_out_degree,max_out_degree))
    cleaned_source_dict = dict()
    cleaned_source_sink_pair = []


    testing_source_ids, test_sink_ids, test_pairs = ut.get_test_file_data(test_file)

    with open(from_file, "r") as f:
        for line in f:
            l = line.replace('\n', '')
            data = l.split('\t')
            outdegree = len(data) - 1
            if outdegree >= min_out_degree and outdegree <= max_out_degree:
                cleaned_source_dict[data[0]] = data[1:]
            elif outdegree > max_out_degree:
                sink_set = set()
                for temp_sink in  data[1:]:
                    if temp_sink in test_sink_ids:
                        sink_set.add(temp_sink)
                sink_sample_set = set(data[1:]).difference(sink_set)
                more_sink = skut.resample(list(sink_sample_set), replace = False, n_samples = max_out_degree - len(sink_set))
                cleaned_source_dict[data[0]] = set(more_sink).union(sink_set)
            elif outdegree < min_out_degree:
                if data[0] in testing_source_ids:
                    cleaned_source_dict[data[0]] = data[1:]


    with open(save_to, 'w') as f, open(save_to + ".pair", 'w') as pf:
        for key,value in cleaned_source_dict.items():
            f.write(key+',')
            f.write("{}\n".format(",".join(value)))
            for sink in value:
                pf.write("{},{}\n".format(key,sink))
                cleaned_source_sink_pair.append((key,sink))

    with open(save_to+".outdegree_range", 'w') as f:
        f.write("{},{}".format(min_out_degree,max_out_degree))

    ut.log("cleaned train data saved in file {}".format(save_to))
    # return cleaned_source_dict, cleaned_source_sink_pair



def make_sink_dict(source_file, to_file, sep=','):
    """
    generate, sorted and save test features into a binary data file
    :param source_ids:
    :param source_file:
    :param to_file:
    :return:
    """

    ut.log("begin to make sink dict from file {} and save to file {}".format(source_file, to_file))
    source_dict = ut.read_as_dict(source_file,sep)

    follow_id_dict = dict()

    for start, sink_list in source_dict.items():
        for sink in sink_list:
            if sink in follow_id_dict:
                follow_id_dict[sink].append(start)
            else:
                follow_id_dict[sink] = [start]

    with open(to_file, 'w') as f:
        for key, value in follow_id_dict.items():
            f.write(key)
            f.write(",")
            f.write(",".join(value))
            f.write("\n")

    print("Totally {} different features.".format(follow_id_dict.__len__()))
    # return follow_id_dict

