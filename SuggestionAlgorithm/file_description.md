



### How To Train Model and Predict
Example: Using `LogisticRegression` with no additional parameters.

The predicted file will be saved to `ut.DATA_DIR + "logical_regression.csv"`
```python
from sklearn.linear_model import LogisticRegression as sklr
import utility as ut
import modelling as m

model = m.train_model( sklr(),ut.SAMPLE_PAIR_WITH_FEATURE_FILE+"5000.csv",2)
m.predict(model,test_file = ut.TEST_FILE_WITH_FEATURES,predice_save_to=ut.DATA_DIR + "logical_regression.csv")
```

### Generated Files

Varibale | Name | Description | Format (per line)
---|---|---|---|
BASE_DIR | $os.path.dirname(__file_path)| base path of this project | -
DATA_DIR | ${BASE_DIR}/data | data folder of this project | -
Train_File |  train.txt| data used for training | `source \t sink01 \t sink02 ...`
Test_Public_File |  test-public.txt| data used for predicting | `Id \t Source \t Sink`
SOURCE_OUTDEGREE_DIST | outdegree_dist.csv | outdegree distribution | `range_of_dist, count_of_sources_with_outdegree_in_this_range`
CLEARNED_ TRAIN_DATA |  cleaned_train _data.csv| data file with wired nodes removed | `source,sink01,sink02...`
SAMPLE_PAIR_WITH_FEATURE_FILE |  sample.with_feature..$N.csv | source sink pair for sample data| `"source","sink","source_indegree","source_outdegree","sink_indegree","sink_outdegree","weighted_share_friends","is_follow"`
PRECALCULATED_ NODE_FEATURES |  pre_calculated _node_features.$N| precalculated nodes features with a particular SOURCE_SINK_MAP file| `node,in_degree, out_degree,closeness centrality,betweenness centrality`
FEATURE_DICT_FILE |  feature_dict.csv| a revert map for train data| `sink, source01, source02...`

