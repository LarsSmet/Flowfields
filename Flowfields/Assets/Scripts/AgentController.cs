using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public GridModifier gridModifier;

    public GameObject agentPrefab;

    public int agentsPerSpawn;

    public float movementSpeed;

    private List<GameObject> agentsInGame;

    private void Awake()
    {
        agentsInGame = new List<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            SpawnAgents();
        }

        if(Input.GetKey(KeyCode.D))
        {
            DeleteAgents();
        }

    }

    private void FixedUpdate()
    {
        if(gridModifier.flowfield == null)
        {
            return;
        }

        foreach(GameObject agent in agentsInGame)
        {
            Cell cellBelow = gridModifier.flowfield.GetCellFromWorldPos(agent.transform.position);
            Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, 0, cellBelow.bestDirection.Vector.y);
            Rigidbody agentBody = agent.GetComponent<Rigidbody>();
            agentBody.velocity = moveDirection * movementSpeed;

        }

    }

    private void SpawnAgents()
    {
        Vector2Int gridSize = gridModifier.gridSize;
        float nodeRadius = gridModifier.cellRadius;
        Vector2 maxSpawnPosition = new Vector2(gridSize.x * nodeRadius * 2 , gridSize.y * nodeRadius * 2 );

        int collisionMask = LayerMask.GetMask("Wall, Agent"); // put layer to agents
        Vector3 newPosition;

        float size = transform.localScale.x;
       

        for(int i = 0; i < agentsPerSpawn; i++)
        {
            GameObject newAgent = Instantiate(agentPrefab);
            newAgent.transform.parent = transform;
            agentsInGame.Add(newAgent);

            do
            {
                newPosition = new Vector3(Random.Range(0, maxSpawnPosition.x), 0, Random.Range(0, maxSpawnPosition.y));
                newAgent.transform.position = newPosition;
            } while (Physics.OverlapSphere(newPosition, size, collisionMask).Length > 0);


        }

     

    
    }
    private void DeleteAgents()
    {
        foreach (GameObject agent in agentsInGame)
        {
            Destroy(agent);
        }
        agentsInGame.Clear();
    }

}
