using UnityEngine;

public interface IWave  {
    Vector3 getPositionOffset(float x, float z, float time);

    void UpdateConfiguration();
}
