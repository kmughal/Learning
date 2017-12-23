# Introduction

How to build offline web sites using service workers. Service worker is introduced in HTML5. The idea is from worker where we move the javascript into it's own thread and html pages request something to the worker. The worker will do something and return the result back to caller.

So there are three types of worker threads :

- web worker ( each web worker has it's own thread)
- shared worker ( thread that can be accessed / used by several browswers)
- service worker