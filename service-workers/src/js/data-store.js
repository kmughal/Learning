var db = new Database();
var $log = new Logger("data-html");

var dbname = "students",
    storename = "records";

function getMockData() {
    return new Promise(function (resolve, reject) {
        fetch("https://jsonplaceholder.typicode.com/albums")
            .then(function (response) {
                response.json()
                    .then(function (j) {
                        resolve(j);
                    }).catch(function (reason) {
                        reject(reason);
                    });
            });
    });
}


function Database() {
    var self = this;
    var request, version = 1, dbname, storename;
    var modes = { readOnly: "readonly", readwrite: "readwrite" };

    function generateUUID() { // Public Domain/MIT
        var d = new Date().getTime();
        if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
            d += performance.now(); //use high-precision timer if available
        }
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
    }

    var createTransaction = function (db) {
        var result = db.transaction(storename, modes.readwrite);
        result.onComplete = function () {
            db.close();
        };
        return result;
    };

    var openStore = function (tx) {
        return tx.objectStore(storename);
    };

    var openDatabase = function (name, storeName) {
        dbname = name;
        storename = storeName;

        return new Promise(function (resolve, reject) {
            request = indexedDB.open(name, version);

            request.addEventListener("upgradeneeded", function (evt) {
                var db = evt.target.result;
                db.createObjectStore(storeName);
                resolve(self);
            });

            request.addEventListener("success", function (evt) {
                var db = evt.target.result;
                var tx = createTransaction(db);
                var store = openStore(tx);
                resolve({ database: db, transaction: tx, store: store });
            });
        });
    };

    var addNewItems = function (values) {
        openDatabase(dbname, storename)
            .then(function (d) {
                if (d.store) {
                    var store = d.store;
                    var arr = (values.length ? values : [values]);
                    console.log(values);
                    arr.forEach(function (item) {
                        item.modified = new Date();
                        store.put(item, item.id);
                    });


                    $log.boldLog(`${arr.length} records added in the index db.`);
                    $log.table(values);
                }
            });
    }

    var find = function (id) {
        return new Promise(function (resolve, reject) {
            openDatabase(dbname, storename)
                .then(function (data) {
                    if (data.store) {
                        var op = data.store.get(parseInt(id));

                        op.onsuccess = function (evt) {
                            resolve(evt.target.result);
                        };

                        op.onerror = function (evt) {
                            reject(evt);
                        };
                    }
                });
        });

    };

    return {
        create: openDatabase,
        add: addNewItems,
        get: find
    };
}

getMockData()
    .then(function (json) {
        db.create(dbname, storename)
            .then(function (result) {
                console.log("%s created with store %s", dbname, storename);
                console.log(result)
                db.add(json);

                $log.boldLog("now lets get a record from store");

                document.querySelector("#btn-search")
                    .addEventListener("click", function (evt) {
                        var q = document.querySelector("#q");
                        db.get(q.value)
                            .then(function (d) {
                                $log.showJson(d || "not found", "search-result");
                            })
                    });
            });

    });


function Logger(id) {
    var _id = "#" + id;
    var render = function (html, overrideId) {
        html = `<div style="padding:10px 10px ;margin:10px 10px;">${html}</div>`;
        _id = overrideId ? "#" + overrideId : _id;
        document.querySelector(_id).innerHTML += html;
    }
    this.log = function (html, id) {
        render(html, id);
    };

    this.boldLog = function (html, id) {
        html = "<b>" + html + "</b>";
        render(html, id);
    };

    this.showJson = function (json, id) {

        render(JSON.stringify(json, null, 2), id);
    }

    this.table = function (data, id) {
        if (data.length) {
            var trs = "";
            data.map(function (item) {
                var tds = "";
                for (let index in item) {
                    tds += `<td>${item[index]}</td>`;
                }
                trs += `<tr>${tds}</tr>`;
            });
            var table = `<table style="border:1px solid blue;" cellpadding="10">${trs}</table>`;
            render(table, id);
        }
    };
}