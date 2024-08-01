import sys
from pytube import Playlist


def get_video_links(playlist_url):
    playlist = Playlist(playlist_url)
    parsed_links = []
    for video in playlist.videos:
        parsed_links.append(video.watch_url)
    return parsed_links


if __name__ == '__main__':
    if sys.argv[1] is None:
        raise Exception("Got null playlist argument")
    else:
        playlist_url = str(sys.argv[1])
    video_links = get_video_links(playlist_url)
    for link in video_links:
        print(link)
