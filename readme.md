
All developper use TPL (Task Process Library) when they use async or call Task returned method.
It is almost easy to do it ... but what if you do not specify async and this code throws an exception...
 
 Well... no task will be returned (no async, nor TaskCompletionSource task managed)...
 
 So you can have exception in code you do not imagine ...
 
 Async Keyword do more than just allowing you to await code. It says to TPL : well just encapsulate my method in a Task and, manage set result with my method returns, or the exception in it.
If you miss it in a method that can Throw an exception... It will be a firework

If you miss the async keyword, YOU HAVE TO RETURN A TASK AND NEVER AN EXCEPTION ... or you will need to try catch arround the method...
To do it you can use 
- a TaskCompletionSource to control the result...
- Task.FromResult(...)
- Task.FromException(...)
- Task.FromCanceled(...)
- an instanciated Task
- the return of Task.Run or Task.StartNew

  >>>AND NOTHING ELSE !!!<<<

this sample demonstrate that... take it, execute in debug both strategies... and compare the callstacks

Enjoy !
