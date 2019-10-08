using UnityEngine;

public class Player : PlayerControl
{
    public delegate void PlayerDeathDel();
    public static event PlayerDeathDel PlayerDeathEvent;

    float h, v;
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        nav.velocity = dir.normalized * nav.speed;
    }
    private void OnDisable()
    {
        PlayerDeathEvent?.Invoke();
    }
}
