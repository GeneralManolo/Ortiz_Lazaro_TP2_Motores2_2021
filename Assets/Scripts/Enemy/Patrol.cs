using UnityEngine;

public class Patrol : Controller_Enemy
{
    private void FixedUpdate()
    {
        //makes the enemy look for the player
        Patroling();
    }

    private void Patroling()
    {
        if (player != null)
        {
            var heading = player.transform.position - this.transform.position;
            var distance = heading.magnitude;
            if (distance < patrolDistance)
            {
                //when the enemy founds the player, it will set it current destination to the enemy as well.
                agent.SetDestination(player.transform.position);
            }
            else
            {
                //if not, the enemy will still search for the player
                PatrolBehaviour();
            }
        }
    }

    private void PatrolBehaviour()
    {
        agent.SetDestination(destination);
        destinationTime -= Time.deltaTime;
        if (destinationTime < 0)
        {
            destination = new Vector3(Random.Range(-10, 12), 1, Random.Range(-12, 9));
            destinationTime = 4;
        }
    }
}
