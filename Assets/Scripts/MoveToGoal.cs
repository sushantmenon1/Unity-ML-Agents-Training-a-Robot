using UnityEngine;
using StarterAssets;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;

public class MoveToGoal : Agent
{
    private StarterAssetsInputs input;
    [SerializeField] private Transform target;
    [SerializeField] private Transform loc;
    private Scene currentScene;
    private int c;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        currentScene = SceneManager.GetActiveScene();
        c = 0;
    }

    // Update is called once per frame
    public override void OnEpisodeBegin()
    {
        if (c>0) { SceneManager.LoadScene(currentScene.name); }
        c++;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int moveX = actions.DiscreteActions[0];
        int moveY = actions.DiscreteActions[1];

        switch (moveX)
        {
            case 0: input.move.x = 0f; break;
            case 1: input.move.x = +1f; break;
            case 2: input.move.x = -1f; break;
        }

        switch (moveY)
        {
            case 0: input.move.y = 0f; break;
            case 1: input.move.y = +1f; break;
            case 2: input.move.y = -1f; break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > 5f)
        {
            AddReward(-0.1f);
        }
        if (distanceToTarget > 10f)
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> act = actionsOut.DiscreteActions;
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: act[0] = 2; break;
            case 0: act[0] = 0; break;
            case +1: act[0] = 1; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: act[1] = 2; break;
            case 0: act[1] = 0; break;
            case +1: act[1] = 1; break;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Goal")
        {
            AddReward(+0.3f);
            Debug.Log("Goal Collided");
            target.localPosition = loc.localPosition + new Vector3(Random.Range(-3f,3f),0.5f,Random.Range(-3f,3f));
            EndEpisode();
        }
       
    }
}