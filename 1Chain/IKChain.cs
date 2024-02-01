using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKChain
{
    // Quand la chaine comporte une cible pour la racine.
    // Ce sera le cas que pour la chaine comportant le root de l'arbre.
    private IKJoint rootTarget = null;
    // Quand la chaine à une cible à atteindre,
    // ce ne sera pas forcément le cas pour toutes les chaines.
    private Transform endTarget = null;
    // Toutes articulations (IKJoint) triées de la racine vers la feuille. N articulations.
    private List<IKJoint> joints = new List<IKJoint>();
    // Contraintes pour chaque articulation : la longueur (à pour
    // ajouter des contraintes sur les angles). N-1 contraintes.
    private List<float> constraints = new List<float>();
    // Un cylndre entre chaque articulation (Joint). N-1 cylindres.
    //private List<GameObject> cylinders = new List<GameObject>();
    // Créer la chaine d'IK en partant du noeud endNode et en remontant jusqu'au noeud plus haut, ou jusqu'à la racine
    public IKChain(Transform _rootNode, Transform _endNode, Transform _rootTarget, Transform _endTarget)
    {
        Debug.Log("=== IKChain::createChain: ===");
        // TO : initialiser les variables membres
        rootTarget = new IKJoint(_rootTarget);
        endTarget = _endTarget;
        
        
        for (Transform tr = _rootNode; tr != _endNode; tr = tr.GetChild(0))
        {
            joints.Add(new IKJoint(tr));
            constraints.Add(Vector3.Distance(tr.position, tr.GetChild(0).position));
        }
        joints.Add(new IKJoint(_endNode));
        // TO: construire la chaine allant de _endNode vers _rootTarget en remontant dans l'arbre (ou l'inverse en descente).
        // Chaque Transform dans Unity a accès à son parent 'tr.parent'
    }
    public void Merge(IKJoint j)
    {

        // TODO-2 : fusionne les noeuds carrefour quand il y a plusieurs chaines cinématiques
        // Dans le cas d'une unique chaine, ne rien faire pour l'instant.
    }
    public IKJoint First()
    {
        return joints[0];
    }
    public IKJoint Last()
    {
        return joints[joints.Count - 1];
    }
    public void Backward()
    {
        Last().SetPosition(endTarget.position);
        for(int i = joints.Count - 2; i >= 0; --i){ 
            joints[i].Solve(joints[i + 1], constraints[i]);
        }
        // TO : une passe remontée de FABRIK. Placer le noeud N-1 sur la cible,
        // puis on remonte du noeud N-2 au noeud 0 de la liste
        // en résolvant les contrainte avec la fonction Solve de IKJoint.
    }
    public void Forward()
    {
        First().SetPosition(rootTarget.position);
        for(int i = 1; i < joints.Count; ++i){  
            joints[i].Solve(joints[i - 1],constraints[i - 1]);
        }
        // TO : une passe descendante de FABRIK. Placer le noeud 0 sur son origine puis on descend.
        // Codez et deboguez déjà Backward avant d'écrire celle-ci.
    }
    public void ToTransform()
    {
        foreach(IKJoint j in joints){
            j.ToTransform();
        }
        // TO: pour tous les noeuds de la liste appliquer la position au transform : voir ToTransform de IKJoint

    }
    public void Check()
    {
        Debug.Log("=== IKChain::Check: ===");
        foreach(IKJoint j in joints){
            Debug.Log(" pos=" + j.position );
        }
        foreach(float c in constraints){
            Debug.Log(" c=" + c);
        }
        // TODO : des Debug.Log pour afficher le contenu de la chaine (ne sert que pour le debug)
        


    }
}