using UnityEngine;
using UnityEngine.VFX; // Required for VFX

public class VFXSpawner : MonoBehaviour
{
    public VisualEffect vfxPrefab; // Assign in Inspector
    public float destroyAfter = 10f; // Time before VFX auto-destroys

    public void SpawnVFXAtPoint(Vector3 spawnPosition)
    {
        // Instantiate VFX at position
        VisualEffect newVFX = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
        
        // Trigger the burst (assuming event name is "OnPlay")
        newVFX.SendEvent("OnPlay"); 
        
        // Destroy after delay
        Destroy(newVFX.gameObject, destroyAfter);
    }
}