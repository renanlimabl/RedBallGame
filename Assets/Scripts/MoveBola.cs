using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBola : MonoBehaviour
{

  public Rigidbody2D rb;
  public float vel;
  public float forcaPulo;

  public bool noChao;

  public float raio;

  public LayerMask layer;

  public ConstantForce2D constantForce2;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    noChao = Physics2D.OverlapCircle(transform.position, raio, layer);

    if (noChao && Input.GetKeyDown(KeyCode.Space))
    {
      rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y + 1) * forcaPulo);
    }
  }

  void FixedUpdate()
  {
    if (rb != null)
    {
      //Chamar um método.
      AplicaForca();
    }
  }

  public void AplicaForca()
  {
    float yVel = rb.velocity.y;
    float xInput = Input.GetAxis("Horizontal");
    float xForca = xInput * vel * Time.deltaTime;


    if (xInput != 0)
    {
      Vector2 forca = new Vector2(xForca, 0);
      rb.AddForce(forca, ForceMode2D.Force);
    }

    if (noChao)
    {
      // No Chão
      if (xInput > 0)
      {
        //pra direita
        constantForce2.torque = -1;
      }
      else if (xInput < 0)
      {
        //pra esquerda
        constantForce2.torque = 1;
      }
      else
      {
        constantForce2.torque = 0;
      }
    }
    else
    // Fora do Chão
    {
      if (xInput > 0)
      {
        constantForce2.torque = -8;
      }
      else if (xInput < 0)
      {
        constantForce2.torque = 8;
      }
      else
      {
        constantForce2.torque = 0;
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, raio);
  }
}
