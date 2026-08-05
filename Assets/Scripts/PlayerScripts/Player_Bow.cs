using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public PlayerMovement playerMovement;

    private Vector2 aimDirection = Vector2.right;//默认朝向为右
    public float shootCooldown = 0.5f; //射击冷却时间


    private float shootTimer = 0;
    public Animator anim;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
            // Shoot();
        }
    }
    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);//把默认层的权重设置为0，隐藏默认层动画
        anim.SetLayerWeight(1, 1);//同时把弓箭层的权重设置为1，显示弓箭层动画
    }
    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);//把默认层的权重设置为1，显示默认层动画
        anim.SetLayerWeight(1, 0);//同时把弓箭层的权重设置为0，隐藏弓箭层动画
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)//如果有输入方向
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);

        }
    }
    // private void Shoot()  这里有几个问题， 1 射击后直接退出动画，没等动画机播放完，2 弓箭较早实例化 ，所以需要再unity 动画里面动态调用方法
    // {
    //     //存储箭矢脚本的引用，可直接修改变量
    //     Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();//Quaternion.identity表示没有旋转，箭矢的方向由箭矢自身的脚本控制
    //     arrow.direction = aimDirection;
    //     shootTimer = shootCooldown; //重置射击计时器
    //     anim.SetBool("isShooting", false);
    // }
    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            //存储箭矢脚本的引用，可直接修改变量
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();//Quaternion.identity表示没有旋转，箭矢的方向由箭矢自身的脚本控制
            arrow.direction = aimDirection;
            shootTimer = shootCooldown; //重置射击计时器
        }
        anim.SetBool("isShooting", false);
        playerMovement.isShooting = false;
    }

}
