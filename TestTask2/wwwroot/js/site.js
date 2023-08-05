window.onload = function () {
    ListInit();
}

function addElement(name, id) {
    list = document.getElementById("ElementList");
    li = document.createElement("li");
    li.innerHTML = `<a href="#">${name}</a>`;
    li.value = id;
    list.appendChild(li);
    li.onclick = function () {
        print(id)
    }
}

function ListInit() {
    var xmlhttp = new XMLHttpRequest();
    var url = `/api/image/files`;
    debugger;
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var elemList = JSON.parse(this.responseText);
            for (i = 0; i < elemList.length; i++) {
                addElement(elemList[i], elemList[i]);
            }
        }

    };

    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function print(id) {
    alert(id);
}