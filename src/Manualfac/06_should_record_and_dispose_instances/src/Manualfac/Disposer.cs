using System;
using System.Collections.Generic;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods
        Stack<IDisposable> needDisposes = new Stack<IDisposable>();
        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */

        public void AddItemsToDispose(object item)
        {
            var disposable = item as IDisposable;
            if (disposable != null)
            {
                needDisposes.Push(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var needDispose in needDisposes)
            {
                needDispose.Dispose();
            }
            needDisposes = null;
        }

        #endregion
    }
}