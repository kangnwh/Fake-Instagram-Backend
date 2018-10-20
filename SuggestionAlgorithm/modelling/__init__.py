import utility as ut
from sklearn.model_selection import KFold
import numpy as np
from sklearn.externals import joblib


def apply_model(model, train_file, test_file, cross_valid_n = 10, predice_save_to=ut.DATA_DIR + "predict.csv"):
    model = train_model(model, train_file,cross_valid_n)
    predict(model,test_file,predice_save_to)

def converToFloatIfPossible(info):
    try:
        return float(info)
    except:
        return info


def train_model(model, train_file, cross_valid_n = 10):
    ut.log("begin to train model...")
    sample_with_feature = ut.read_as_list(train_file)
    kf = KFold(cross_valid_n, True)

    X_list = list()
    y_list = list()
    for line in sample_with_feature[1:]:
        features = list(map(converToFloatIfPossible, line[2:-1]))
        X_list.append(features)
        y_list.append(line[-1])

    X = np.array(X_list)
    y = np.array(y_list)

    test_result = list()
    for train_index, test_index in kf.split(X):
        X_train, X_test = X[train_index], X[test_index]
        y_train, y_test = y[train_index], y[test_index]

        model.fit(X_train, y_train)
        score = model.score(X_test, y_test)
        test_result.append(score)
        ut.log("Test #{} => Score: {}".format(len(test_result) , score))

    return model



def predict(model, test_file, predice_save_to=ut.DATA_DIR + "predict.csv"):

    ut.log("begin to predict from file {}...".format(test_file))
    test_with_feature = ut.read_as_list(test_file)
    X_list = list()

    for line in test_with_feature[1:]:
        features = list(map(float, line[2:-1]))
        X_list.append(features)

    X = np.array(X_list)

    result = model.predict_proba(X)

    positive_class_idx = None
    classes = model.classes_
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
        prob = prob if prob > 0.001 else 0.01
        predict_list.append([i+1, prob])

    ut.log("saving predict result to file {}...".format(predice_save_to))
    ut.write_list_csv(predice_save_to,predict_list)

    ut.log("saving model to file {}...".format(predice_save_to+".model.pkl"))
    joblib.dump(model, predice_save_to+".model.pkl")


