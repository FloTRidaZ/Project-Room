using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler,  IDragHandler{
	public GameObject player;
	public Camera cam;
	private Inventory inventory;
	private bool isPlaying;
	private Quaternion origin;
	private float deltaY;
	private float deltaX;

	void Start () {
		inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory> ();
		origin = player.transform.rotation;
	}


	public void OnPointerDown (PointerEventData eventData) {
		Vector3 pos = new Vector3 (eventData.position.x, eventData.position.y);
		Ray ray = cam.ScreenPointToRay (pos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 2f)){
			GameObject hittedObj = hit.collider.gameObject;
			toDetermine (hittedObj);
		}
	}

	public void OnDrag(PointerEventData eventData){
		deltaY += eventData.delta.y;
		deltaX += eventData.delta.x;
		Quaternion rotationY = Quaternion.AngleAxis (deltaX/4f, Vector3.up);
		Quaternion rotationX = Quaternion.AngleAxis (-deltaY/4f, Vector3.right);

		player.transform.rotation = origin * rotationY;
		cam.transform.rotation = origin * player.transform.rotation * rotationX;
	}

	private void toDetermine (GameObject obj){
		if (obj.GetComponent <Animation> ()) {
			animHandle (obj.GetComponent<Animation> (), obj.GetComponent<State> (), obj.tag);
		}
		if (obj.GetComponent<Item> ()) {
			inventory.AddItem (obj);
		}
	}

	private void animHandle (Animation anim, State state, string objTag){
		isPlaying = anim.isPlaying;
		if (!isPlaying && !state.IsOpen ()) {
			anim.Play ("open" + objTag);
			state.ToOpen ();
		} else if (!isPlaying && state.IsOpen ()) {
			anim.Play ("close" + objTag);
			state.ToClose ();
		}
	}
		
}