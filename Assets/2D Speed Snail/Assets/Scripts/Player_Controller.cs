using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    //Conponents
    public Rigidbody2D _rb;

    private float _moveSpeed;
    private float _jumpHeight;
    private bool _isJumping;
    private float _horizontalMove;
    private float _verticalMove;
    public bool _isGrounded;


    //animations
    Animator _animator;
    string _currState;
    const string Snail_Idle = "Snail_Idle";
    const string Snail_Run = "Snail_Run";
    const string Snail_Jump = "Snail_Jump";
    const string Snail_Fall = "Snail_Fall";

    // Start is called before the first frame update
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _moveSpeed = 0.5f;
        _jumpHeight = 60f;
        _isJumping = false;
        _isGrounded = true;

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _verticalMove = Input.GetAxisRaw("Vertical");
        if(Input.anyKey == false){
            AnimationState(Snail_Idle);
        }
    }

    private void FixedUpdate(){ 
        if(_horizontalMove > 0.1f || _horizontalMove < -0.1f){
            _rb.AddForce(new Vector2(_horizontalMove * _moveSpeed/2f, 0f), ForceMode2D.Impulse);
            if(_isGrounded){
                AnimationState(Snail_Run);
            }
        }
        if(!_isJumping && _verticalMove > 0.1f){
            _rb.AddForce(new Vector2(0f, _verticalMove * _moveSpeed), ForceMode2D.Impulse);
           
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            _isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            _isGrounded = false;
        }
    }

    private void AnimationState(string newAnim){
        if (newAnim == _currState){
            return;
        }
        _animator.Play(newAnim);
        _currState = newAnim;
    }

    bool IsAnimationPlaying(Animator animator, string stateName){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f){
            return true;
        }
        else{
            return false;
        }
    }

}