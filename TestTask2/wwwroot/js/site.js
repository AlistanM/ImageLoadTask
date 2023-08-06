window.onload = function () {
    ListInit();
    initEvent()

}
function initEvent() {
    load = document.getElementById("load")
    save = document.getElementById("save")
    del = document.getElementById("del")
    load.onchange = loadImage
}

function loadImage() {
    let file = this.files[0];
    filename = document.getElementById("name")
    descript = document.getElementById("descript")
    filename.value = file.name
    descript.value = ""
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

function updateList() {
    list = document.getElementById("ElementList");
    list.innerHTML = ""
    for (i = 0; i < files.length; i++) {
        addElement(files[i].name, files[i].id)
    }
}

var files = []

function ListInit()
{
    files.length = 0
    var xmlhttp = new XMLHttpRequest();
    var url = `/api/image/files`;
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var elemList = JSON.parse(this.responseText);
            for (i = 0; i < elemList.length; i++) {
                files.push({ id: elemList[i].id, name: elemList[i].name, descript: elemList[i].descript })
            }
            updateList()
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function print(id) {
    alert(id);
}

