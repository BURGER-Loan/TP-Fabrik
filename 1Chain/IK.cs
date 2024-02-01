using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{

    // Le transform (noeud) racine de l'arbre,
    // le constructeur créera une sphère sur ce point pour en garder une copie visuelle.
    public GameObject rootNode = null;
    // Un transform (noeud) (probablement une feuille) qui devra arriver sur targetNode
    public Transform srcNode = null;
    // Le transform (noeud) cible pour srcNode
    public Transform targetNode = null;
    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;
    // Toutes les chaines cinématiques
    public List<IKChain> chains = new List<IKChain>();
    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 10;

    void Start()
    {
        if (createChains)
        {
            Debug.Log("(Re)Create CHAIN");
            createChains = false; // la chaîne est créée une seule fois, au début
                                  // DO :
                                  // Création des chaînes : une chaîne cinématique est un chemin entre deux nœuds carrefours.
                                  // Dans la 1ere question, une unique chaine sera suffisante entre srcNode et rootNode.

            GameObject copyRoot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            copyRoot.transform.position = rootNode.transform.position;
            chains.Add(new IKChain(rootNode.transform, srcNode, copyRoot.transform, targetNode));


            // TODO-2 : Pour parcourir tous les transform d'un arbre d'Unity vous pouvez faire une fonction récursive
        // ou utiliser GetComponentInChildren comme ceci :
        // foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        // TODO-2 : Dans le cas où il y a plusieurs chaines, fusionne les IKJoint entre chaque articulation.
        }
    }

    
    void Update()
    {
        Debug.Log("=== IK::Update ===");
        if (createChains)
            Start();

        for(int i = 0; i < nb_ite; i++){
            for(int j = 0 ; j < chains.Count; j++){
                Debug.Log("=== IK::backward ===");
                chains[j].Backward();
                chains[j].ToTransform();
            }
        }
        for(int l = 0; l < nb_ite; l++){
            for(int m = 0 ; m < chains.Count; m++){
                Debug.Log("=== IK::forward ===");
                chains[m].Forward();
                chains[m].ToTransform();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Chains count=" + chains.Count);
            foreach (IKChain ch in chains)
                ch.Check();
        }
    }

    void IKOneStep(bool down)
    {
        int j;
        if(down){
            for (j = 0; j < nb_ite; ++j)
            {
                foreach(IKChain chain in chains){
                    chain.Backward();
                    chain.ToTransform();
                }
            }
        }else{
            for (j = 0; j < nb_ite; ++j)
            {
                foreach(IKChain chain in chains){
                    chain.Forward();
                    chain.ToTransform();
                }
            }
        }
    }
}