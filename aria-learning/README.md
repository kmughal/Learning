# Introduction

Aria (Accessibility rich internet applications) are set of properties or standards which makes web contents more accessible to disable people.

HTML5 introduces semantic web elements which we can use to write reasonable html. But we have seen on the web where developers uses `<div>` tags to build buttons. It is very hard for the people who has visual problems. Also there are few new bits which are not supported in the few browsers. In order to rescue we (developers) can use `WAI-ARIA`.

## WAI-ARIA

These are set of additonal properties which we can add to html elements. There are mainly three items:

- Role: this defines the type / nature / purpose of the element
- Properties: these are the properties of the element which gives extra semantics such as `aria-required=true`
- State: this refers to the state of the element `aria-checked=true` is the aria state which can be used for radio button or check box.

## Few examples

### A simple page example

```html
    <header>
        <h1>Test page</h1>
        <nav role="navigation">
            <ul>...</ul>
            <form role="search>
            <!-- Search box -->
            </form>
        </nav>
    </header>
    <main>
        <article role="article>
        </article>

        <aside role="complementary"> ... </aside>
    </main>
    <footer> .. </footer>
```

In above examples we have used semantic elements which clearly shows the original intend of the section. Also you have notices few role attributes which is refers to the purpose of the html element.

### A search box

``` html

<input 
type="text"
id="q"
aria-label="Search movies"
name="q"
/>
```

`aria-label` attribute refers to what is the reason we used this text-box though we have not specified a label but with this aria-property we are following the accessibility patterns.

***Note*** ( if a web site is build using `<div>` tags then you should use the aria attributes to give proper semantics.)

### How to notify a user when a dynamic contents are updated / changes ?

There is an aria property which can be used for this purpose i.e. `aria-live`, possible values are:

- off: this is the default value, means don't do anything
- polite: update the user only if user is idle
- assertive: announce updates as soon as poosible
- rude: inform user about the change regardless of user activity

`<section id='ajax-result' aria-live="rude">...</section>` it is nice to read out the heading once the update happens in order to do so user an extra property (`aria-atomic="ture"`)

### Form validations

Form validations / errors must have an aria attribute (role="alert" and aria-relevant="all") means that all the area should be read out the full message.
