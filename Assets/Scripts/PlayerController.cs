
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GroundedActorController
{
    private float _turnSmoothVelocity;
    private Vector3 _velocity;

    public PlayerParameters Parameters { get; private set; }

    protected new void Start()
    {
        base.Start();
        Parameters = GetComponent<PlayerParameters>();
    }

    protected new void Update()
    {
        if (Parameters.Dead)
        {
            return;
        }
        
        base.Update();
        
        if (MainWeapon is not null)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                MainWeapon.MainAttackStart();
            }
            
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                MainWeapon.MainAttackEnd();
            }
        }
        
        if (SecondaryWeapon is not null)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                SecondaryWeapon.MainAttackStart();
            }
            
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                SecondaryWeapon.MainAttackEnd();
            }
        }
    }

    protected new void FixedUpdate()
    {
        if (Parameters.Dead)
        {
            return;
        }
        
        base.FixedUpdate();
        
        var x = Input.GetAxisRaw("Horizontal");
        Move((int)x);
        
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet") && 
            !col.gameObject.CompareTag(gameObject.tag))
        {
            BulletKnockback(col);
        }
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            Parameters.TakeDamage(PlayerStatsManager.Instance.stats.basePlayerDamage);
            HitKnockback(new Vector2(Facing == ActorFacing.Right ? 1.0f : -1.0f, 0.0f));
        }
    }

    public override bool TakeItem(ItemsMeta.Item item)
    {
        return InventoryManager.Instance.Store(item);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        BlackoutScript.instance.OnFadeIn += OnFadeIn;
        BlackoutScript.instance.FadeIn();
    }
    
    private void OnFadeIn()
    {
        BlackoutScript.instance.OnFadeIn -= OnFadeIn;
        
        // Reload the scene
        SceneManager.LoadScene("Scenes/UpgradeScreen");
    }
}