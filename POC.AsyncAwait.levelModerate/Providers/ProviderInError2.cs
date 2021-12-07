using System;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Task{Payload} factory method and (no async defined), the factory method throws an exception directly.
    /// So, no task will be returned. Exception must the catched directly when calling GetValueAsync.
    /// This usage is not a good practice... it is dangerous...
    /// </summary>
    internal sealed class ProviderInError2 : IProvider
    {
        #region Explication
        // This code is a Task<Payload> Factory method, and do not returns directly a Payload value : async is missing in the method signature.
        // Exception return is allowed by compiler. Because : I can control the way my Task is created or the technical algorithm I used (maybe with a TaskCompletionSource, or by instanciating another task, etc.)
        // Here, the code throws an Exception instead of Task creation... 
        // Task Process Library will not take the control (no async defined), the consumer will fail directly when calling GetValueAsync (no task returned)
        // Consumer must set a try catch strategy over GetValueAsync to catch this...
        // Here its easy, you can see the exception, but imagine a code where you use an encapsulated webservice call that fail...
        // Best practice will be to do as ProviderInError4

        #endregion

        public Task<Payload> GetValueAsync()
        {
            throw new Exception($"Bwaaa from {nameof(ProviderInError2)}");
        }
    }
}
