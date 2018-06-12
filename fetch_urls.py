from bs4 import BeautifulSoup
import requests
import lxml
import pdb
import re

def run():
    url = "https://www.raspbian.org/RaspbianMirrors"
    page = requests.get(url, timeout=5)
    if page.status_code != 200:
        raise Exception
    soup = BeautifulSoup(page.text, 'lxml')
    table = soup.find("tbody")
    urls = []
    for idx1, row in enumerate(table.find_all("tr")):
        if idx1 > 0:
            for idx2, col in enumerate(row.find_all("td")):
                if idx2 == 3:
                    link = col.text
                    link = re.sub(r".*:\/\/", '', link)
                    link = re.sub(r"\/.*", '', link)
                    urls.append(link)

    with open("mirrors.txt", "w+") as f:
        for url in urls:
            f.write(url + "\n")



if __name__=="__main__":
    run()
