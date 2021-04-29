public class Following : Controller_Enemy
{
    private void FixedUpdate()
    {
        FollowingBehaviour();
    }

    private void FollowingBehaviour()
    {
        if (player != null )
        {
            //considers the player as an agent and follows it.
            agent.SetDestination(player.transform.position);
        }
    }
}
