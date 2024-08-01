import sys
from pytube import YouTube


def GetLink(id):
    print(YouTube("https://youtube.com/watch?v=" + id).streams.first().url)


if __name__ == '__main__':
    if sys.argv[1] is None:
        raise Exception("Got null link argument")
    else:
        GetLink(str(sys.argv[1]))
