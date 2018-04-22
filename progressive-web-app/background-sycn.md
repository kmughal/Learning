# Backgorund Sycn

Sometimes you have connection but that connection is too weak that actually you are disconnected. 
So how we can do background sync.

## Steps involve in setting this up !

- Setup service worker
- Store the data to some browser storage such as IndexDb , local storage etc.
- Once data is store then :

```js
    if ("serviceworker" in navigator && "SyncManager" in window) {
        navigator.serviceworker.ready.then( e => {
            return sw.sync.register("name-tag").then(updateUi);
        })
    }


    // now in the service worker file

    self.addEventHandler("sync",e => {
        if (e.tag === "name-tag") {
            event.waitUntil(/*write your code to sync info*/);
        }
    });
```