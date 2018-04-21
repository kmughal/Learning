# Web App Menifest

This is one principle of adding a web app to the home screen. This is already possible but now with
W3C standard it is more like a standard for us. [web app menifest](https://www.w3.org/TR/appmanifest/)

You can visit [pwa rocks](https://pwa.rocks/) it has collection of sites which were build with that mindset.

## Where to add the menifest file

You can add this menifest file to the head tag of the html pag

```html
<link rel="menifest" href="/menifest.json" >
````

## What goes inside the menifest file

It is better to categories this for your ease as:

- Identity ( name , short_name)
- Presentation (start_url, theme_color, background_color, orientation, display)
- Icons
- Misc properties ( dir {"direction of text ltr / rtr are possible values"} , lang , description,scope,service worker)
- Applications (related_applications, prefer_related_applications,screenshots)

## Programmatic events

There are two events which are for the developers to play with:

- "appinstalled" -> window.addEventListener("appinstalled", evt => {});
- "beforeinstallprompt" -> can be useful to get stats for user choice.

## Sample Menifest

```json

{
  "name": "Sample menifest",
  "short_name": "sample",
  "icons": [
    {
      "src": "assets/front/images/gn-icons/16x16.png",
      "sizes": "16x16",
      "type": "image/png"
    },
    {
      "src": "assets/front/images/gn-icons/32x32.png",
      "sizes": "32x32",
      "type": "image/png"
    },
    {
      "src": "assets/front/images/gn-icons/57x57.png",
      "sizes": "57x57",
      "type": "image/png"
    },
    {
      "src": "assets/front/images/gn-icons/60x60.png",
      "sizes": "60x60",
      "type": "image/png"
    },
    {
      "src": "assets/front/images/gn-icons/72x72.png",
      "sizes": "72x72",
      "type": "image/png"
    }
  ],
  "start_url": ".",
  "display": "standalone",
  "orientation": "portrait",
  "background_color": "#ffffff",
  "theme_color": "#0D3975"
}

```

## Urls to look

- [W3C Application Menifest](https://www.w3.org/TR/appmanifest/)
- [Auto manifest generator](https://realfavicongenerator.net/)
- [PWA Rock](https://pwa.rocks/)