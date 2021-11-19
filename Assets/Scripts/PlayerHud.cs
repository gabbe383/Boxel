using Unity.Netcode;
using Unity.Collections;
using TMPro;

public class PlayerHud : NetworkBehaviour
{
    private NetworkVariable<NetworkString> playersName = new NetworkVariable<NetworkString>();

    private bool overlaySet = false;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            playersName.Value = $"player {OwnerClientId}";
        }
    }

    public void SetOverlay()
    {
        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerOverlay.text = playersName.Value;
    }

    private void Update()
    {
        if(!overlaySet && !string.IsNullOrEmpty(playersName.Value))
        {
            SetOverlay();
            overlaySet = true;
        }
    }
}