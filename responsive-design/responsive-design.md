# Responsive Design
- With media queries you can include break points by using :

```css
@media screen and (min-width:300px) and (max-width:600px) {

}
```

this approach is not better. A more roboust approach whould be target the medium screen and then go to large screen:

```css

@media screen and (min-width:500px) {

}

@media screen and (min-width:1140px) {

}
```

- The idea is that we will have saperate css files to target small , medium and large devices. The css file for small deivces will have the maximum rules defined where medium and large will have only bits which are different from small device. So this is the approach which is much better.

## Semantic Design

HTML5 Semantic tags are helpful for the search engines, site is more accessible. Several tags were added such as article, header, nav etc still you can use the span and div for other general purposes. Another important thing is "way finding" means how your site is designed so it is easy for the person who is looking on your site to find what he / she wants.

## CSS Preprocessors

JS,Images,CSS and Media objects are the assets used in HTML then given to browers. CSS preprocessors enables us to use loop,import,variables,functions (to do some calculations),partials,nesting to write better and fast css.Even we can minify this as well which can improve the performance of the site as assets being very light weight.