# Raspbian Mirror Select
Lists the fastest mirrors based on ping from the list at https://www.raspbian.org/RaspbianMirrors
## Usage
* Generate the `mirrors.txt` file by running `python3 fetch_urls.py`.
* Run `mirror_select.exe` in the same directory as the generated `mirrors.txt`. Alternatively, compile it yourself.
* Check for mirror issues on the list page before updating `/etc/apt/sources.list`.