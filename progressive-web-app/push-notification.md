# Push Notifications


There are manily two types of notifications

- Non persistant notifications
- Presistant notifications


## Non persistant notifications

First of all we will see if user has granted the permission then you can simply show the notification if not then you prompt for access.

```js

if (Notification.permission === "grant") {
    showNewNotification();
    return;
}

if (Notification.permission === "denied") {
    showPrompt();
    return;
}

function showPrompt() {
    Notification.requestPermission().then(function(r){
        if (r === "granted") {
            showNewNotification();
        }
    });
}

function showNewNotification() {

    var notification = new Notification("Security updates" , {
        body : "body text",
        badge : "",
        icon : "",
        image : "",
        tag : "" ,
        renotify : false,
        actions : [{ // buttons which you can put in the notificatoin
            action : "id",
            title : "action title",
            icon : ""
        }],
        silent : true ,
        sound : "path",
        vibrate : [100,50,200],
        dir : "text_direction ltr/rtr",
        lang : "en-US",
        timestamp : Date.now()
    } )
}

```

## Persistant Notifications

There are similar as non's one but they are related wih service workers.

```js

self.registration.showNotification() ;// registration is referring to the service worker

// to handle clicks and events we add those on the service worker
self.addEventListener("notificationonclick",evt => {
    if (!evt.action) {
        // user click on the body of the notification ...
    }
});
```

## Push notifications

### Generate VAPID key

You can visit [Push companion](https://web-push-codelab.glitch.me/)


## Urls to look

- [Notification generator](https://tests.peter.sh/notification-generator/)

```js
// service worker and push notification supported 
if ("serviceworker" in navigator && "pupshManager" in window) {

// check if user has a subscribed already
var SW = null;
navigator.serviceworker.ready().then (sw => {
    SW = sw;
    sw.pushManager.getSubscriber().then(s => {
        if (s) {

        } else {
            // no subscription.
        }
    })
})
}

document.querySelector("#btn-subscribe")
.addEventListener("click", e => {
    this.disabled = ture;

    SW.pushManager.getSubscriber().then(s=> {
        if (s === null) {
            SW.pushManager.subscribe({userVisibleOnly : ture,applicationServerKey :urlB64ToUint8Array(""/*hash key*/)}).then(s => { /* you can now send that to your server */})
        } else {
             SW.pushManager.unSubscribe();
        }
    })

})
//https://github.com/GoogleChromeLabs/web-push-codelab/blob/master/app/scripts/main.js
function urlB64ToUint8Array(base64String) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding)
    .replace(/\-/g, '+')
    .replace(/_/g, '/');

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}

```

### Send Messages

- First of all we have to create an encrypted message. Encrption can be hard so instead of that we go to [web-push-lib on git hub](https://github.com/web-push-libs/web-push) and see the javascript piece.
- Second step is to send this message to a message server so it follows http push specs.
- Third step is that message can be send to the browser
- Fourth step is that you will handle this push message via service worker

````js
sw.addEventListener("push" , evt => {
    var payload = evt.data.json();
    evt.waitUntil(xxx);

});
```