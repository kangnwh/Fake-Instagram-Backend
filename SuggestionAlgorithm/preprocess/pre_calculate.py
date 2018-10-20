import utility as ut
from operator import itemgetter
import networkx as nx

OUTDEGREE_RANGE = [0, 100, 1000, 10000, 100000, 1000000]

OUTDEGREE_RANGE_TITLES = [
    "A",
    "B",
    "C",
    "D",
    "E",
    "F"
]


def get_range(count):
    for i in range(len(OUTDEGREE_RANGE) - 1):
        if count >= OUTDEGREE_RANGE[i] and count < OUTDEGREE_RANGE[i + 1]:
            return i

    return len(OUTDEGREE_RANGE) - 1

class calculator:



    def __init__(self,source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set , max_outdegree = 20000,min_outdegree = 3 ):
        self.source_dict, self.source_dict_keys_set, self.sink_dict, self.sink_dict_keys_set  = \
        source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set

        self.max_outdegree = max_outdegree
        self.min_outdegree = min_outdegree

    def _calculate_pair_features(self,start,end, positive = True):
        this_pair = [start, end]

        # source node features
        source_indegree = len(self.sink_dict[start]) if start in self.sink_dict else 0
        source_outdegree = len(self.source_dict[start]) if start in self.source_dict else 0

        # source indegree & indegree level
        this_pair.append(source_indegree)
        this_pair.append(get_range(source_indegree))

        # source outdegree & outdegree level
        this_pair.append(source_outdegree)
        this_pair.append(get_range(source_outdegree))

        # sink node features
        sink_indegree = len(self.sink_dict[end]) if end in self.sink_dict else 0
        sink_outdegree = len(self.source_dict[end]) if end in self.source_dict else 0

        # sink indegree & indegree level
        this_pair.append(sink_indegree)
        this_pair.append(get_range(sink_indegree))

        # sink outdegree & outdegree level
        this_pair.append(sink_outdegree)
        this_pair.append(get_range(sink_outdegree))

        # shared friend weighted
        w_in, w_out = self._shared_weighted(start, end)
        this_pair.append(w_in)
        this_pair.append(w_out)

        # start node order index by indegree across all nodes it following
        # this_pair.append(self._indegree_order(start))
        #
        # # start node order index by outdegree across all nodes it following
        # this_pair.append(self._outdegree_order(start))
        #
        # # end node order index by indegree across all nodes it following
        # this_pair.append(self._indegree_order(end))
        #
        # # end node order index by outdegree across all nodes it following
        # this_pair.append(self._outdegree_order(end))

        # round2 shared friend weighted
        # TODO too big for round2
        # w2_in, w2_out = _round2_shared_weighted(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set, start, end)
        # this_pair.append(w2_in)
        # this_pair.append(w2_out)

        # negative flag
        this_pair.append(1) if positive else this_pair.append(0)
        return this_pair


    def _indegree_order(self,node):
        """
        
        :return:
        """
        # TODO
        source_indegree_count = len(self.sink_dict[node]) if node in self.sink_dict_keys_set else 0
        order_index = 1

        node_set = {node}

        if node in self.source_dict_keys_set:
            for sink in self.source_dict[node]:
                if len(self.sink_dict[sink]) > source_indegree_count:
                    order_index += 1
                    node_set.add(sink)

        if node in self.sink_dict_keys_set:
            for source in self.sink_dict[node]:
                if source in self.sink_dict_keys_set:
                    if source not in node_set and len(self.sink_dict[source]) > source_indegree_count:
                        order_index += 1
                        node_set.add(source)

        return order_index

    def _outdegree_order(self,node):
        source_outdegree_count = len(self.source_dict[node]) if node in self.source_dict_keys_set else 0
        order_index = 1
        node_set = {node}

        if node in self.sink_dict_keys_set:
            for source in self.sink_dict[node]:
                # if source in self.source_dict_keys_set:
                if source not in node_set and len(self.source_dict[source]) > source_outdegree_count:
                    order_index += 1
                    node_set.add(source)

        if node in self.source_dict_keys_set:
            for sink in self.source_dict[node]:
                if sink in self.source_dict_keys_set:
                    if sink not in node_set and len(self.source_dict[sink]) > source_outdegree_count:
                        order_index += 1
                        node_set.add(sink)


        return order_index






    # @ut.func_run_time
    def _shared_weighted(self,start, end):
        in_score = 0
        out_score = 0

        if start not in self.source_dict_keys_set or end not in self.sink_dict_keys_set:
            return 0,0

        if end in self.source_dict[start]:
            out_score = 99999

        if end in self.sink_dict:
            if start in self.sink_dict[end]:
                in_score = 99999
        else:
            return 0 , out_score

        if len(self.source_dict[start]) > self.max_outdegree or len(self.sink_dict[end]) > self.max_outdegree:
            return in_score, out_score


        if start in self.source_dict_keys_set:
            source_following = set(self.source_dict[start])
        else:
            source_following = set()

        if end in self.sink_dict_keys_set:
            sink_followed = set(self.sink_dict[end])
        else:
            sink_followed = set()

        shared = source_following.intersection(sink_followed)


        for node in shared:
            if node in self.source_dict_keys_set:
                out_score += 1 / len(self.source_dict[node])

            if node in self.sink_dict_keys_set:
                in_score += 1 / len(self.sink_dict[node])

        in_score = in_score if in_score > 0.001 else 0.001
        out_score = out_score if out_score > 0.001 else 0.001
        return in_score , out_score

    # @ut.func_run_time
    def _round2_shared_weighted(self, start, end):
        source_following = set(self.source_dict[start])
        sink_followed = set(self.sink_dict[end])

        source_round2 = set()
        sink_round2 = set()

        for node in source_following:
            if node in self.source_dict_keys_set:
                source_round2 = source_round2.union(set(self.source_dict[node]))

            if node in self.sink_dict_keys_set:
                source_round2 = source_round2.union(set(self.sink_dict[node]))

        for node in sink_followed:
            if node in self.source_dict_keys_set:
                sink_round2 = sink_round2.union(set(self.source_dict[node]))

            if node in self.sink_dict_keys_set:
                sink_round2 = sink_round2.union(set(self.sink_dict[node]))

        source_round2 = source_round2.union(source_following)
        sink_round2 = sink_round2.union(sink_followed)
        shared = source_round2.intersection(sink_round2)

        in_score = 0
        out_score = 0

        for node in shared:
            if node in self.source_dict and len(self.source_dict[node]) > 0:
                out_score += 1 / len(self.source_dict[node])

            if node in self.sink_dict and len(self.sink_dict[node]) > 0:
                in_score += 1 / len(self.sink_dict[node])

        return in_score, out_score




# def precalculate_node_features(positive_pairs, save_to):
#     # node,in_degree, out_degree,closeness centrality,betweenness centrality, pagerank
#     ut.log("begin to pre-calculate features for nodes and save data to {}...".format(save_to))
#
#     #source_sink_pair, min_outdegree, max_outdegree = clean.read_source_sink_pair(sample_file= from_file)
#
#     g = Graph.TupleList(positive_pairs, directed=True, vertex_name_attr='name', edge_attrs=None, weights=False)
#
#     node_feature_dict = dict()
#
#     for node in zip(g.vs,g.indegree(),g.outdegree(),g.closeness(),g.betweenness(),g.pagerank()):
#         node_feature_dict[node[0]['name']] = [node[1],node[2],node[3],node[4],node[5]]
#
#     with open(save_to, 'w') as w:
#         for key,value in node_feature_dict.items():
#             w.write(key+',')
#             w.write("{}\n".format(",".join(value)))
#
#     return node_feature_dict

#
#
# def _path_indegree(path,g):
#     score = 0 if len(path) > 0 else -1
#     for p in path:
#         score += g.vs[p].indegree()
#
#     return score
#
# def _path_outdegree(path,g):
#     score = 0 if len(path) > 0 else -1
#     for p in path:
#         score += g.vs[p].outdegree()
#
#     return score
#
# def _path_closeness(path,g):
#     score = 0 if len(path) > 0 else -1
#     for p in path:
#         score += g.vs[p].closeness()
#     return score
#
#
# def _path_betweeness(path,g):
#     score = 0 if len(path) > 0 else -1
#     for p in path:
#         score += g.vs[p].betweenness()
#     return score
#
# def _path_pagerank(path,g):
#     score = 0 if len(path) > 0 else -1
#     for p in path:
#         score += g.vs[p].pagerank()
#     return score
#
# def _path_vertex_disjoint_paths(path,g):
#     pass
#
# #@ut.func_run_time
# def calculate_pair_features(start,end,g,log=False):
#     if log:
#         ut.log("calculating pair {}=>{}...".format(start, end))
#     try :
#         path = g.get_shortest_paths(start, end)[0]
#         return _path_indegree(path, g), \
#                _path_outdegree(path, g), \
#                _path_closeness(path, g), \
#                _path_betweeness(path, g), \
#                _path_pagerank(path, g)
#     except:
#         ut.log("start {} or end {} is not in the sample file".format(start,end))
#         return 0,0,0,0,0
#
#
#
# def read_precalculate_node_features(from_file):
#     node_feature_list = []
#     with open(from_file,'r') as f:
#         for line in f:
#             l = line.replace('\n','')
#             data = l.split(',')
#             node_feature_list.append(tuple(data))
#
#
# def igraph_from_pair_file(train_featured_file):
#     pairs = []
#     with open(train_featured_file,'r') as f:
#         for line in f:
#             l = line.replace('\n','')
#             data = l.split(',')
#             assert data.__len__() >= 3, "make sure the featured file have at least 3 columns: source,sink,is_follow"
#             if data[-1] == 1:
#                 pairs.append((data[0],data[1]))
#
#     g = Graph.TupleList(pairs, directed=True, vertex_name_attr='name', edge_attrs=None, weights=False)
#     return g
#
#
# #@ut.func_run_time
# def _calculate_pair_features(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set, start, end, positive:bool):
#
#     this_pair = [start,end]
#
#     # source node features
#     source_indegree = len(sink_dict[start]) if start in sink_dict else 0
#     source_outdegree = len(source_dict[start]) if start in source_dict else 0
#     this_pair.append(source_indegree)
#     this_pair.append(source_outdegree)
#
#     # sink node features
#     sink_indegree = len(sink_dict[end]) if end in sink_dict else 0
#     sink_outdegree = len(source_dict[end]) if end in source_dict else 0
#     this_pair.append(sink_indegree)
#     this_pair.append(sink_outdegree)
#
#     # shared friend weighted
#     w_in, w_out = _shared_weighted(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set, start, end)
#     this_pair.append(w_in)
#     this_pair.append(w_out)
#
#     # round2 shared friend weighted
#     # TODO too big for round2
#     # w2_in, w2_out = _round2_shared_weighted(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set, start, end)
#     # this_pair.append(w2_in)
#     # this_pair.append(w2_out)
#
#     # negative flag
#     this_pair.append(0) if positive else this_pair.append(1)
#     return this_pair
#
# #@ut.func_run_time
# def _shared_weighted(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set,  start, end):
#
#     source_following = set(source_dict[start])
#     sink_followed = set(sink_dict[end])
#
#     shared = source_following.intersection(sink_followed)
#     in_score = 0
#     out_score = 0
#
#     for node in shared:
#         if node in source_dict_keys_set:
#             out_score += 1/len(source_dict[node])
#
#         if node in sink_dict_keys_set:
#             in_score += 1/len(sink_dict[node])
#
#     return in_score, out_score
#
# #@ut.func_run_time
# def _round2_shared_weighted(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set, start, end):
#     source_following = set(source_dict[start])
#     sink_followed = set(sink_dict[end])
#
#     source_round2 = set()
#     sink_round2 = set()
#
#     for node in source_following:
#         if node in source_dict_keys_set:
#             source_round2 = source_round2.union(set(source_dict[node]))
#
#         if node in sink_dict_keys_set:
#             source_round2 = source_round2.union(set(sink_dict[node]))
#
#     for node in sink_followed:
#         if node in source_dict_keys_set:
#             sink_round2 = sink_round2.union(set(source_dict[node]))
#
#         if node in sink_dict_keys_set:
#             sink_round2 = sink_round2.union(set(sink_dict[node]))
#
#     source_round2 = source_round2.union(source_following)
#     sink_round2 = sink_round2.union(sink_followed)
#     shared = source_round2.intersection(sink_round2)
#
#     in_score = 0
#     out_score = 0
#
#     for node in shared:
#         if node in source_dict and len(source_dict[node]) >0:
#                 out_score += 1 / len(source_dict[node])
#
#         if node in sink_dict and len(sink_dict[node])>0:
#             in_score += 1 / len(sink_dict[node])
#
#     return in_score, out_score
