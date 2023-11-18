using GrupoI;
using Navigation.Interfaces;
using Navigation.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aEstrellaMovement : INavigationAlgorithm
{
    public enum Directions
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    private WorldInfo _mundo;
    private List<Nodo> _listaAbierta = new List<Nodo>();
    private List<Nodo> _listaCerrada = new List<Nodo>();
    private bool meta = false;
    


    public void Initialize(WorldInfo informacionMundo, INavigationAlgorithm.AllowedMovements movimientosPermitidos)
    {
        _mundo = informacionMundo;
        Debug.Log("iniciado!!");
    }

    public CellInfo[] GetPath(CellInfo startNode, CellInfo targetNode)
    {
        // Nodo en el que empieza el muñequito.
        Nodo nodoInicial = new Nodo(_mundo, startNode, null);   // El padre del nodo actual es null

        _listaAbierta.Add(nodoInicial);      // Añadimos el estado inicial a la lista abierta

        Nodo actual;

        while (meta)
        {
            // Calcular distancia manhattan de los posibles sucesores del nodo a la meta
            Debug.Log("While iniciado!!");

            actual = _listaAbierta[0];
            _listaAbierta.RemoveAt(0);  // Eliminamos dicho elemento de la lista abierta

            // Metemos todos los nodos que hemos visitado en la lista cerrada.
            _listaCerrada.Add(actual);


        }

        Debug.Log("Distancia a la meta: "+actual.getInfoCelda().Distance(_mundo.Exit, CellInfo.DistanceType.Euclidean));

        CellInfo[] path = new CellInfo[1];
        path[0] = _mundo[16,16];
        return path;

    }

}
