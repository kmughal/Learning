var version = "v1";
self.addEventListener("install", function (event) {

    console.log("install started");
    event.waitUntil(caches.open(version)
        .then(function (cache) {
            return cache.addAll(["offline.html"]
            );
        }));
    self.skipWaiting();
});

self.addEventListener("activate", function (event) {
    console.log("active started %s", version);
});

self.addEventListener("fetch", function (event) {
    if (!navigator.onLine) {

        event.respondWith(
            caches.match(event.request)
            .then(function(res) {
                if (res) {
                    console.log(res)
                    return res;
                }
               return caches.match(new Request("offline.html"));
            })
        );
    } else {
        event.respondWith(fetchAndUpdate(event.request));
    }

});


function fetchAndUpdate(request) {
    return fetch(request).then(function (res) {
        if (res) {
            return caches.open(version)
                .then(function (cache) {
                    return cache.put(request, res.clone())
                        .then(function () {
                            return res;
                        });
                });
        }
    })
}
