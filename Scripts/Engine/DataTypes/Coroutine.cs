using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game
{
    class Coroutine
    {
        // Create a method that returns an IEnumerator<T> object
        public IEnumerator<int> CountToTen()
        {
            // Loop from 1 to 10
            for (int i = 1; i <= 10; i++)
            {
                // Yield the current value
                yield return i;
            }
        }

        public void use()
        {
            // Use the coroutine
            IEnumerator<int> coroutine = CountToTen();
            while (coroutine.MoveNext())
            {
                // Print the current value
                Console.WriteLine(coroutine.Current);
            }
        }
    }
}
