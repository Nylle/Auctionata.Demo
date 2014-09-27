using System;
using System.Collections;
using System.Collections.Generic;

namespace Auctionata.Demo.Monads
{
    /// <summary>
    /// This code is lifted from Mark Seemann's excellent blog: http://blog.ploeh.dk/2011/02/04/TheBCLalreadyhasaMaybemonad/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Maybe<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> values;

        public Maybe()
        {
            values = new T[0];
        }

        public Maybe(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            values = new[] {value};
        }

        public static Maybe<T> Empty
        {
            get { return new Maybe<T>(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
