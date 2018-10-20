import os
import time
__file_path = os.path.dirname(__file__)

BASE_DIR = os.path.dirname(__file_path)

DATA_DIR = "{base}{sep}data{sep}".format(base=BASE_DIR, sep=os.path.sep)

ORIGINAL_TRAIN_FILE = DATA_DIR + "train.txt"

ORIGINAL_TEST_FILE = DATA_DIR + "test-public.txt"
TEST_SINK_INFO = DATA_DIR + "test-public_sink_info.csv"
TEST_FILE_WITH_FEATURES = DATA_DIR + "test-public_with_features."

SOURCE_OUTDEGREE_DIST = DATA_DIR + "outdegree_dist.csv"


MIN_OUTDEGREE_FOR_CLEAN = 1

MAX_OUTDEGREE_FOR_CLEAN = 1000

CLEARNED_TRAIN_FILE = DATA_DIR + "cleaned_train_data.csv"

PRECALCULATED_NODE_FEATURES = DATA_DIR + "pre_calculated_node_features."

FEATURE_DICT_FILE = DATA_DIR + "feature_dict.csv"

SAMPLE_PAIR_FILE = DATA_DIR + "sample."

SAMPLE_PAIR_WITH_FEATURE_FILE = DATA_DIR + "sample.with_feature."

SAMPLE_FILE = DATA_DIR + "sample"



def log(info, end='\n'):
    print(info,end=end,flush=True)



def write_list_csv(to_file:str,data:list):

    with open(to_file, 'w') as f:
        for l in data:
            if type(l) == list:
                s = map(str,l)
                f.write("{}\n".format(','.join(s)))
            else:
                f.write(l)
                f.write("\n")


def read_as_dict(from_file,sep=','):
    data = dict()
    with open(from_file, 'r') as f:
        for line in f:
            l = line.replace('\n','')
            d = l.split(sep)
            data[d[0]] = d[1:]
    return data

def write_dict_csv(to_file,data:dict):
    with open(to_file, 'w') as f:
        for key,value in data.items():
            if type(value) == list:
                f.write("{},{}\n".format(key,",".join(value)))
            else:
                f.write("{},{}\n".format(key, value))


def read_as_list(from_file,sep=','):
    data = list()
    with open(from_file, 'r') as f:
        for line in f:
            l = line.replace('\n','')
            d = l.split(sep)
            data.append(d)
    return data


def _increaseDict(dict, key):
    value = 1
    if key in dict:
        value = dict[key] + 1

    return value


def func_run_time(func):

    def innderfunc(*args, **kwargs):
        start_time = time.time()
        log("func {} start at {}".format(func.__name__, start_time))
        result = func(*args, **kwargs)
        end_time = time.time()
        log("func {} cost {}".format(func.__name__, end_time - start_time))
        return result


    return innderfunc

def get_test_file_data(from_file):

    log("begin to read source ids need prediction from test file {}...".format(from_file))
    test_source_ids = set()
    test_sink_ids = set()
    test_pair = list()
    with open(from_file, "r") as f:
        f.readline()  # header
        for line in f:
            l = line.replace('\n', '')
            data = l.split('\t')
            test_source_ids.add(data[1])
            test_sink_ids.add(data[2])
            test_pair.append([data[1],data[2]])

    return test_source_ids, test_sink_ids, test_pair