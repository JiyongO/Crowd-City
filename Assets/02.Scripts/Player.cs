using UnityEngine;

public class Player : PlayerControl
{
    public delegate void PlayerDeathDel();
    public static event PlayerDeathDel PlayerDeathEvent;

    float h, v;
    private void FixedUpdate()
    {
#if UNITY_EDITOR
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        nav.velocity = dir.normalized * nav.speed;
#elif UNITY_ANDROID
        if (Input.GetMouseButtonUp(0))
        {
            h = 0;
            v = 0;
        }
        if (h == 0 && v == 0)
        {
            h = Input.GetTouch(0).deltaPosition.x;
            v = Input.GetTouch(0).deltaPosition.y;
        }
        Vector3 _dir = new Vector3(h, 0, v);
        nav.velocity = _dir.normalized * nav.speed;
#endif
    }
    private void OnDisable()
    {
        PlayerDeathEvent?.Invoke();
    }
}
