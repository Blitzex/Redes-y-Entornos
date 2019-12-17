using System.Collections;
using UnityEngine.Networking;
using UnityEngine;


namespace S3
{
    public class PlayerController : NetworkBehaviour{

        public GameObject bulletPrefab;
        public Transform bulletSpawn;

        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdFire();
            }
        }

        [Command]
        void CmdFire()
        {
            //Crea las balas desde el prefab
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            //Añade velocidad al objeto
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;

            //Spawnea las balas para los clientes
            NetworkServer.Spawn(bullet);

            //Destruye la bala pasados dos segundos
            Destroy(bullet, 2);
        }

        public override void OnStartLocalPlayer()
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}