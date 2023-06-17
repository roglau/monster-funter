using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public float speed = 0.025f;
    public float jumpForce = 5f;
    public AudioSource jump, footstep, land, hitsound,skill1s, skill2s;
    public GameObject p, flamethrower,particle;
    public Rigidbody rb;
    public Slider stamina,health, skill1, skill2;
    public Transform firePos;
    public GameObject projectile, freeLook, aim, crosshair, hud;
    public Cinemachine.CinemachineImpulseSource source;
    public Rig rig;
    public GameObject dScreen;

    private PlayerObject instance = PlayerObject.GetInstance();

    public Inventory[] inventory;
    public Image itemSelected, item;
    public TextMeshProUGUI itemName,itemQuantity;
    private int selectedInventory = 0;

    private float? jumpTime, dodgeTime, landTime, drinkTime, rageTime; 
    private float jumpPeriod, dodgePeriod, landPeriod, drinkPeriod, skill1Time, skill2Time;

    private float attackSpeed = 0.5f;
    private float nextFireTime = 0f;
    private static int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float maxComboDelay = 1;

    private bool isJumping = false, collided = false;

    public Camera cam;

    private Vector3 dest;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jumpPeriod = 0.7f;
        dodgePeriod = 0.8f;
        landPeriod = 0.1f;
        drinkPeriod = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.GetCurrHp() == 0 && !instance.Died)
        {
            animator.SetBool("Death", true);
            Invoke("Death", 1f);
        }
        
        speed = 0.05f;
        if (Time.time - rageTime <= 5)
        {
            speed = 0.125f;
        }
        footstep.pitch = 0.4f;
        if (instance.GetIdx() == 0)
        {
            animator.SetBool("Wizard", true);
            animator.SetBool("Paladin", false);
        }
        else
        {
            animator.SetBool("Wizard", false);
            animator.SetBool("Paladin", true);
        }

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Debug.Log(vertical);
        //Debug.Log(horizontal);
        Vector3 movement = vertical * transform.forward + horizontal * transform.right;
        //Debug.Log(movement);

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (hud.activeSelf)
            {
                hud.SetActive(false);
            }
            else
            {
                hud.SetActive(true);
            }
        }

        if (animator.GetBool("Wizard"))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                rig.weight = 1;
                animator.SetBool("isAim",true);
                crosshair.SetActive(true);
                freeLook.SetActive(false);
                aim.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    particle.SetActive(true);
                    Invoke("DoneFire", 1.5f);
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    //Debug.Log(ray);
                    RaycastHit hit;
                    //if(Physics.Raycast(firePos.position, firePos.forward, out hit))
                    if (Physics.Raycast(ray, out hit))
                    {
                        dest = hit.point;
                    }
                    else
                    {
                        dest = ray.GetPoint(1000);
                    }

                    //Debug.Log("oit" + ray +" des"+dest);

                    var projectileObj = Instantiate(projectile, firePos.position, Quaternion.identity) as GameObject;
                    projectileObj.SetActive(true);

                    projectileObj.GetComponent<Rigidbody>().velocity = (dest - firePos.position).normalized * 20;
                    hitsound.Play();
                    source.GenerateImpulse(cam.transform.forward);
                    if(instance.CurrMission == 0 && NpcInteractable.progress[instance.CurrMission] < 10 && instance.OnProgress)
                    {
                        NpcInteractable.progress[instance.CurrMission]++;
                    }
                }
            }
            else
            {
                rig.weight = 0;
                animator.SetBool("isAim", false);
                crosshair.SetActive(false);
                freeLook.SetActive(true);
                aim.SetActive(false);
            }
            

            if (Input.GetKeyDown(KeyCode.F) && skill2.value == 10)
            {
                skill2Time = Time.time;
                skill2.value = 0;
                skill2s.Play();
                animator.SetBool("Fire", true);
                Invoke("ActiveFire", 1.2f);
                Invoke("DoneFire", 4f);
                if (!NpcInteractable.skill2 && instance.CurrMission == 1 && instance.OnProgress)
                {
                    NpcInteractable.skill2 = true;
                    NpcInteractable.progress[instance.CurrMission]++;
                }
            }
            if(Input.GetKeyDown(KeyCode.R) && animator.GetBool("isFlying"))
            {
                FlyingFalse();
            }

            if (Input.GetKeyDown(KeyCode.R) && skill1.value == 10 && !animator.GetBool("isFlying"))
            {
                collided = false;
                skill1Time = Time.time;
                skill1.value = 0;
                var pos = instance.P.transform.position;
                pos.y = 9f;
                instance.P.transform.position = pos;
                animator.SetBool("isFlying", true);
                Invoke("FlyingFalse", 5f);
                jump.Play();

                if (!NpcInteractable.skill1 && instance.CurrMission == 1 && instance.OnProgress)
                {
                    NpcInteractable.skill1 = true;
                    NpcInteractable.progress[instance.CurrMission]++;
                }
            }

            if (animator.GetBool("isFlying"))
            {
                var pos = instance.P.transform.position;
                pos.y = 9f;
                instance.P.transform.position = pos;

                transform.position += transform.forward * 0.3f;
            }

            if (skill1.value < 10)
            {
                skill1.value = (Time.time - skill1Time) * 10 / instance.FlyCd;
            }
            else
            {
                skill1Time = 0f;
            }

            if (skill2.value < 10)
            {
                skill2.value = (Time.time - skill2Time) * 10 / instance.FireCd;
            }
            else
            {
                skill2Time = 0f;
            }
        }

        if (animator.GetBool("Paladin"))
        {
            crosshair.SetActive(false);
            if(Time.time - rageTime >= 5)
            {
                attackSpeed = 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.R) && skill1.value == 10 && !animator.GetBool("isRage"))
            {
                skill1s.Play();
                skill1Time = Time.time;
                skill1.value = 0;
                animator.SetBool("isRage", true);
                rageTime = Time.time;
                attackSpeed = 0.3f;
                speed = 0.125f;
                if (!NpcInteractable.skill1 && instance.CurrMission == 1 && instance.OnProgress)
                {
                    NpcInteractable.skill1 = true;
                    NpcInteractable.progress[instance.CurrMission]++;
                }
            }
            else
            {
                animator.SetBool("isRage", false);
            }

            if (Input.GetKeyDown(KeyCode.F) && skill2.value == 10)
            {
                skill2s.Play();
                skill2Time = Time.time;
                skill2.value = 0;
                animator.SetBool("isRoll", true);
                if (!NpcInteractable.skill2 && instance.CurrMission == 1 && instance.OnProgress)
                {
                    NpcInteractable.skill2 = true;
                    NpcInteractable.progress[instance.CurrMission]++;
                }
                StartCoroutine(Roll());
            }
           
            if(Time.time > skill2Time + 2f && animator.GetBool("isRoll"))
            {
                animator.SetBool("isRoll", false);
                StopCoroutine(Roll());
            }


            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                animator.SetBool("isAttack1", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                animator.SetBool("isAttack2", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                animator.SetBool("isAttack3", false);
                noOfClicks = 0;
            }
            if (Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
            }
            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick();
                }
            }

            if (skill1.value < 10)
            {
                skill1.value = (Time.time - skill1Time) * 10 / instance.RageCd;
            }

            if (skill2.value < 10)
            {
                skill2.value = (Time.time - skill2Time) * 10 / instance.RollCd;
            }

        }
        

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    float interactRange = 2f;
        //    Collider[] arr = Physics.OverlapSphere(transform.position, interactRange);
        //    foreach(Collider c in arr)
        //    {
        //        if(c.TryGetComponent(out NpcInteractable npcInteractable))
        //        {
        //            npcInteractable.Interact();
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log("Inventory length"+inventory.Length+"selected"+selectedInventory);
            //Debug.Log("inventory[0]" + inventory[0].itemName);
            selectedInventory++;
            if (selectedInventory >= inventory.Length)
            {
                selectedInventory = 0;
                itemSelected.sprite = inventory[selectedInventory].itemSprite;
                itemQuantity.text = instance.GetMeat().ToString();
                item.sprite = inventory[selectedInventory + 1].itemSprite;
                itemName.text = inventory[selectedInventory].itemName;
            }
            else
            {
                itemSelected.sprite = inventory[selectedInventory].itemSprite;
                itemQuantity.text = instance.GetHealthPotion().ToString();
                item.sprite = inventory[selectedInventory - 1].itemSprite;
                itemName.text = inventory[selectedInventory].itemName;
            }
            //Debug.Log("idx" + selectedInventory);
            
        }
        if(selectedInventory==0)
        itemQuantity.text = instance.GetMeat().ToString();
        else
        itemQuantity.text = instance.GetHealthPotion().ToString();

        //if (Input.GetKeyDown(KeyCode.Space) && animator.GetBool("isGrounded"))
        //{
        //    jump.Play();
        //    jumpTime = Time.time;
        //    animator.SetBool("isJumping", true);
        //    animator.SetBool("isGrounded", false);
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}

        //if (Time.time >= jumpTime + jumpPeriod)
        //{
        //    //land.Play();
        //    animator.SetBool("isJumping", false);
        //    animator.SetBool("isGrounded", true);
        //    jumpTime = 0f;
        //}

        if (!animator.GetBool("Fire"))
        {
            if (Input.GetKeyDown(KeyCode.G) && animator.GetBool("isGrounded") && CheckItem())
            {
                animator.SetBool("isDrinking", true);
                drinkTime = Time.time;

            }
            if (Time.time >= drinkTime + drinkPeriod && animator.GetBool("isDrinking"))
            {
                animator.SetBool("isDrinking", false);
                drinkTime = 0f;
                if (selectedInventory == 0)
                {
                    instance.SetMeat(instance.GetMeat() - 1);
                    stamina.value = 100;
                }
                else
                {
                    instance.SetHealthPotion(instance.GetHealthPotion() - 1);
                    instance.SetCurrHp(instance.GetCurrHp()+50);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                jump.Play();
                animator.SetTrigger("Jump");
                animator.SetBool("isGrounded", false);
                isJumping = true;
            }

            if (Time.time >= landTime + landPeriod)
            {
                animator.SetBool("isGrounded", true);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                dodgeTime = Time.time;
                animator.SetBool("isDodge", true);
                StartCoroutine(Dodge());
            }

            if (Time.time >= dodgeTime + dodgePeriod && animator.GetBool("isDodge"))
            {
                animator.SetBool("isDodge", false);
                dodgeTime = 0f;
                //transform.position -= transform.forward * 1.8f;
                StopCoroutine(Dodge());
            }

            if (Input.GetKey(KeyCode.LeftShift) && movement.magnitude >= 0.1f && stamina.value != 0)
            {
                //footstep.Play();
                footstep.pitch = 1f;
                speed = 0.2f;
                animator.SetBool("isRunning", true);

            }
            else if (Input.GetKey(KeyCode.LeftShift) && movement.magnitude >= 0.1f && stamina.value == 0)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isMoving", true);

            }
            else if (!Input.GetKey(KeyCode.LeftShift) && movement.magnitude == .0f)
            {
                footstep.enabled = false;
                animator.SetBool("isMoving", false);
                animator.SetBool("isRunning", false);

            }
            else if (!Input.GetKey(KeyCode.LeftShift) && movement.magnitude >= 0.1f)
            {
                footstep.pitch = 0.6f;
                animator.SetBool("isRunning", false);
                animator.SetBool("isMoving", true);

            }
            if(movement.magnitude > 0f)
            {
                footstep.enabled = true;
            }

            transform.position += movement * speed;
            //Debug.Log(transform.position);

            animator.SetFloat("posY", vertical);
            animator.SetFloat("posX", horizontal);
        }

        
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("isAttack1", true);
            hitsound.Play();
            if (instance.CurrMission == 0 && NpcInteractable.progress[instance.CurrMission] < 10 && instance.OnProgress)
            {
                NpcInteractable.progress[instance.CurrMission]++;
            }
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            animator.SetBool("isAttack1", false);
            animator.SetBool("isAttack2", true);
            hitsound.Play();
            if (instance.CurrMission == 0 && NpcInteractable.progress[instance.CurrMission] < 10 && instance.OnProgress)
            {
                NpcInteractable.progress[instance.CurrMission]++;
            }
        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animator.SetBool("isAttack2", false);
            animator.SetBool("isAttack3", true);
            //transform.position += transform.forward * 2f;
            hitsound.Play();
            if (instance.CurrMission == 0 && NpcInteractable.progress[instance.CurrMission] < 10 && instance.OnProgress)
            {
                NpcInteractable.progress[instance.CurrMission]++;
            }
        }
    }

    private void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        dScreen.SetActive(true);
        Time.timeScale = 0f;
        instance.Died = true;
    }

    IEnumerator Roll()
    {
        float duration = 2f;
        float speed = 1f;
        float distance = 2f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.forward * distance;

        float t = 0f;
        while (t < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t / duration);
            t += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPosition;
        yield return new WaitForSeconds(duration / 2f);
        dodgeTime = Time.time;
    }

    IEnumerator Dodge()
    {
        float duration = 0.8f;
        float speed = 1f;
        float distance = 2f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position - transform.forward * distance;

        float t = 0f;
        while (t < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t / duration);
            t += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPosition;
        yield return new WaitForSeconds(duration / 2f);
        dodgeTime = Time.time;
    }

    public void FlyingFalse()
    {
        if (!collided)
        {
            animator.SetTrigger("Land");
            animator.SetBool("isFlying", false);
            land.Play();
        }
    }

    public void ActiveFire()
    {
        flamethrower.SetActive(true);
        particle.SetActive(true);
    }

    public void DoneFire()
    {
        animator.SetBool("Fire", false);
        flamethrower.SetActive(false);
        particle.SetActive(false);
    }

    
    public bool CheckItem()
    {
        if (selectedInventory == 0)
        {
            if (instance.GetMeat() > 0) return true;
        }
        else
        {
            if (instance.GetHealthPotion() > 0) return true;
        }

        return false;
    }

    public void ApplyJumpForce()
    {
        rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping)
        {
            landTime = Time.time;
            isJumping = false;
            animator.SetTrigger("Land");
            land.Play();
        }

        if (animator.GetBool("isFlying"))
        {
            collided = true;
            animator.SetTrigger("Land");
            animator.SetBool("isFlying", false);
            land.Play();
        }
    }

}
