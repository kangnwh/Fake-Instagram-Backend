import os

__file_path = os.path.dirname(__file__)

BASE_DIR = os.path.dirname(__file_path)

DATA_DIR = "{base}{sep}data{sep}stage_data{sep}".format(base=BASE_DIR, sep=os.path.sep)
