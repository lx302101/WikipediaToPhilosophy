# WikipediaToPhilosophy

From a page in wikipedia (Ex. Science), it is possible to get to the philosophy page by following this alogrithm:

Click on the first non-parenthesized, non-italicized link (Ignore external links, links to the current page)), and repeating this step on the new page until the pages loop, reach a dead end, or reach philosophy.

Ex. for Science, the process is 

science -> scientific method -> empirical evidence -> information -> uncertainty -> epistemology -> philosophy

Program.cs was the initial algorithm. It was implemented into ASP.Net Core website through MVC framework. THe data was then represented through vertical treees using D3.js
