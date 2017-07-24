using System;
using System.Collections.Generic;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods
        readonly ISet<IDisposable> needDisposes = new HashSet<IDisposable>();
        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */

        public void AddItemsToDispose(object item)
        {
            var disposable = item as IDisposable;
            if (disposable != null)
            {
                needDisposes.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var needDispose in needDisposes)
            {
                needDispose.Dispose();
            }
        }

        #endregion
    }
}