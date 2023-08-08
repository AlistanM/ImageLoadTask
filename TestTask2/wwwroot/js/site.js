window.onload = function () {
    ListInit();
    initEvent()

}
function initEvent() {
    load = document.getElementById("load")
    save = document.getElementById("save")
    del = document.getElementById("del")
    load.onchange = loadImage
    del.onclick = delImage
    save.onclick = saveImage
}

function loadImage() {
    let file = this.files[0];
    filename = document.getElementById("name")
    descript = document.getElementById("descript")
    filename.value = file.name
    descript.value = ""
}

var selectedid = null

function delImage() {
    filename = document.getElementById("name")
    descript = document.getElementById("descript")
    image = document.getElementById("Image")
    filename.value = ""
    descript.value = ""
    image.src = ""
    var xmlhttp = new XMLHttpRequest();
    var url = `/api/image/delete?id=${selectedid}`;
    xmlhttp.onreadystatechange = function ()
    {
        if (this.readyState == 4 && this.status == 200) {
            ListInit()
        }
    }
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function saveImage() {
    let xmlhttp = new XMLHttpRequest();
    let xmlhttp2 = new XMLHttpRequest();
    filename = document.getElementById("name")
    descript = document.getElementById("descript")
    input = document.getElementById("load")

    if (input.value)
    {
        var body = JSON.stringify({
            name: filename.value,
            description: descript.value
        })

        let url = `/api/image/save`
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                console.log("File successfully uploaded!");
            }
        };
        xmlhttp.open("POST", url, true);
        xmlhttp.setRequestHeader("Content-Type", "application/json;");
        xmlhttp.send(body);


        let url2 = `/api/image/saveimage`
        xmlhttp2.onreadystatechange = function () {
            if (xmlhttp2.readyState == 4 && xmlhttp2.status == 200) {
                // The file has been uploaded successfully
                console.log("File successfully uploaded!");
            }
        };
        var reader = new FileReader();
        reader.readAsDataURL(input.files[0]);
        reader.onload = function () {
            var resultStr = reader.result;
            xmlhttp2.open("POST", url2, true)
            xmlhttp2.setRequestHeader("Content-Type", "application/json;");
            xmlhttp2.send(resultStr);
        };
    }

    else {
        var body = JSON.stringify({
            id: selectedid,
            name: filename.value,
            description: descript.value
        })
        var url = `/api/image/update`
        xmlhttp.open("POST", url, true);
        xmlhttp.setRequestHeader("Content-Type", "application/json;");
        xmlhttp.send(body);
    }

    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            ListInit()
        }
    }

    input.value = ""
    filename.value = ""
    descript.value = ""
}


function addElement(name, id) {
    list = document.getElementById("ElementList");

    li = document.createElement("li");
    li.innerHTML = `<a href="#">${name}</a>`;
    li.value = id;
    list.appendChild(li);
    li.onclick = function () {
        filename = document.getElementById("name")
        descript = document.getElementById("descript")
        info = files.find(x => { return x.id == id })
        filename.value = info.name
        descript.value = info.descript
        selectedid = id
        var xmlhttp = new XMLHttpRequest();
        var url = `/api/image/getimage?id=${selectedid}`;

        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                imageData = this.responseText
                document.getElementById("Image").src = "data:image/png;base64," + imageData;
            }
        }

        xmlhttp.open("GET", url, true);
        xmlhttp.send();

        this.getElementsByTagName("a").className = "active"
    }
}

function updateList() {
    list = document.getElementById("ElementList");
    li = list.getElementsByTagName("li")
    while (li.length > 0) {
        li[0].remove()
    }
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
                files.push({ id: elemList[i].id, name: elemList[i].name, descript: elemList[i].description })
            }
            updateList()
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function disableSelect() {
    list = document.getElementById("ElementList")
    li = list.getElementsByTagName("li")
    for (i - 0; i < li.length; i++) {
        li[i].getElementsByTagName("a")[0].className = ""
    }
}
