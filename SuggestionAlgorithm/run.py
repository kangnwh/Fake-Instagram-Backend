

import os,sys
import random
cur_path = os.getcwd()

parent_path = os.path.dirname(cur_path)

sys.path.append(cur_path)
sys.path.append(parent_path)


from sklearn.ensemble import RandomForestClassifier as rfc
import modelling as m
import utility as ut
from sklearn.model_selection import KFold
from sklearn.externals import joblib
from sklearn.utils import shuffle, resample
import numpy as np
from preprocess import pre_calculate




def make_out_of_package(positive_count,source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set):
    ut.log("generate out of package pair features...")

    # positive_count = len(sample_source_pair_list)
    out_of_package_count = 0
    out_of_package_count_target = positive_count
    original_train_pair = ut.read_as_list(ut.ORIGINAL_TRAIN_FILE, "\t")
    source_count = len(original_train_pair)



    calc = pre_calculate.calculator(source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set)
    out_of_package_pairs = list()

    while out_of_package_count < out_of_package_count_target:

        row_idx = random.randint(0, source_count - 1)
        row = original_train_pair[row_idx]
        if len(row) < 3:
            continue
        sink_idx = random.randint(1, len(row) - 1)

        source = row[0]
        sink = row[sink_idx]
        this_pair = calc._calculate_pair_features(source, sink, True)
        this_pair.extend([99999,0,0,0])
        out_of_package_pairs.append(this_pair)
        out_of_package_count += 1
        if out_of_package_count % 1000 == 0:
            ut.log("{}/{}".format(out_of_package_count, out_of_package_count_target))

    return out_of_package_pairs


clf = rfc(n_estimators=60, min_samples_split=90,
          min_samples_leaf=10, max_features='sqrt', oob_score=True,criterion='entropy')
train_file = ut.DATA_DIR + "sample.with_feature.10000.networx.csv"
test_file=ut.DATA_DIR + "test-public_with_features.10000.networx.csv"
predice_save_to=ut.DATA_DIR + "kn_randomforestclassifier.2.csv"

# all source
source_dict = ut.read_as_dict(ut.ORIGINAL_TRAIN_FILE,"\t")
sink_dict = ut.read_as_dict(ut.DATA_DIR + "sink_dict.csv")
source_dict_keys_set = set(sink_dict.keys())
sink_dict_keys_set = set(sink_dict.keys())

used_columns = [4,5,6,8,10]

ut.log("begin to train model...")
sample_with_feature = ut.read_as_list(train_file)[1:]
sample_with_feature = sample_with_feature + make_out_of_package(int(len(source_dict_keys_set)/10),source_dict, source_dict_keys_set, sink_dict, sink_dict_keys_set)

ut.write_list_csv(ut.DATA_DIR + "last_hope.csv",sample_with_feature)
# sample_count = len(sample_with_feature) - 1
# mid_idx = int(sample_count * 0.5)
# P_80 = int(sample_count * 0.8)
# P_20 = int(sample_count * 0.2)

kf = KFold(2, True)

X_list = list()
y_list = list()

shuffled_sample = shuffle(sample_with_feature[1:])

for line in shuffled_sample:
    features = list(map(float, [line[i] for i in used_columns]))
    X_list.append(features)
    y_list.append(line[-1])

X = np.array(X_list)
y = np.array(y_list)
test_result = list()
for train_index, test_index in kf.split(X):
    X_train, X_test = X[train_index], X[test_index]
    y_train, y_test = y[train_index], y[test_index]
    clf.fit(X_train, y_train)
    score = clf.score(X_test, y_test)
    test_result.append(score)
    ut.log("Test #{} => Score: {}".format(len(test_result), score))


ut.log("begin to predict from file {}...".format(test_file))
test_with_feature = ut.read_as_list(test_file)
X_list = list()


for line in test_with_feature[1:]:
    features = list(map(float, [line[i] for i in used_columns]))
    X_list.append(features)

X_for_test = np.array(X_list)

result = clf.predict_proba(X_for_test)

positive_class_idx = None
classes = clf.classes_
ut.log("classes: {}".format(classes))
for i in range(len(classes)):
    if classes[i] == '1':
        positive_class_idx = i
        break
ut.log("#{} is the wanted column".format(positive_class_idx+1))

predict_list = list()
predict_list.append(["Id","Prediction"])



for i in range(len(result)):
    prob = result[i][positive_class_idx]
    start = test_with_feature[i][0]
    end  = test_with_feature[i][1]
    if start in source_dict and end in source_dict[start]:
        predict_list.append([i + 1, 1])
    else:
        predict_list.append([i + 1, prob])




ut.log("saving predict result to file {}...".format(predice_save_to))
ut.write_list_csv(predice_save_to,predict_list)

ut.log("saving model to file {}...".format(predice_save_to+".model.pkl"))
joblib.dump(clf, predice_save_to+".model.pkl")

