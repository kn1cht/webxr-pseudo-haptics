using UnityEngine;

namespace WebXRPseudo.FineRoughness {
    public static class Perturber
    {
        public static float perturber(float alpha, float velocity, float scale = 0.05f)
        {
            return alpha * Random.Range(-1f, 1f) * velocity * scale;
        }
    }
}
