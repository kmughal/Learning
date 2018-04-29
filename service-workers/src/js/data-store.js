
class Logger {

    constructor(id) {
        this._id = "#" + id;
    }

    render(html, overrideId) {
        html = `<div style="padding:10px 10px ;margin:10px 10px;">${html}</div>`;
        this._id = overrideId ? "#" + overrideId : this._id;
        document.querySelector(this._id).innerHTML += html;
    }

    log(html, id) {
        this.render(html, id);
    }


    boldLog(html, id) {
        this.render("<b>" + html + "</b>", id);
    }

    showJson(json, id) {
        this.render(JSON.stringify(json, null, 2), id);
    }

    table(data, id) {
        if (data.length) {
            const table = `<table style="border:1px solid blue;" cellpadding="10">${data.map(function (item) {
                var tds = "";
                for (let index in item) {
                    tds += `<td>${item[index]}</td>`;
                }
                return `<tr>${tds}</tr>`;
            }).join("")}</table>`;
            this.render(table, id);
        }
    };
}


const dbname = "students";
const storename = "records";
const db = new Database(dbname, storename);
const $log = new Logger("data-html");

const getMockData = function () {
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
};


function Database(dbName, storeName) {
    var self = this;
    var request, version = 1, dbname, storename;
    const modes = { readOnly: "readonly", readwrite: "readwrite" };
    const _dbname = dbName;
    const _storename = storeName;

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
        const result = db.transaction(_storename, modes.readwrite);
        result.onComplete = function () {
            db.close();
        };
        return result;
    };

    const openStore = (tx) =>
        tx.objectStore(_storename);

    const openDatabase = () =>
        new Promise(function (resolve, reject) {
            request = indexedDB.open(_dbname, version);

            request.addEventListener("upgradeneeded", function (evt) {
                var db = evt.target.result;
                db.createObjectStore(_storename);
                resolve(self);
            });

            request.addEventListener("success", function (evt) {
                var db = evt.target.result;
                var tx = createTransaction(db);
                var store = openStore(tx);
                resolve({ database: db, transaction: tx, store: store });
            });
        });

    const addNewItems = (values) =>
        openDatabase()
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

    const find = (id) =>
        new Promise(function (resolve, reject) {
            openDatabase()
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