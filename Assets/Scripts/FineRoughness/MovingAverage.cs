using System.Collections.Generic;

namespace WebXRPseudo.FineRoughness {
    public class MovingAverage
    {
        private int n = 16;
        private float sum;
        private Queue<float> queue = new Queue<float>();

        public float average(float latest)
        {
            sum += latest;
            queue.Enqueue(latest);

            if (queue.Count > n)
            {
                sum -= queue.Dequeue();
            }

            return sum / queue.Count;
        }
    }
}
