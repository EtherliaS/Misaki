import sys
from pytube import YouTube


def Download(id):
    YouTube("https://youtube.com/watch?v=" + id).streams.first().download(output_path="./Music", skip_existing=True, filename=id)


if __name__ == '__main__':
    if sys.argv[1] is None:
        raise Exception("Got null link argument")
    else:
        Download(str(sys.argv[1]))
