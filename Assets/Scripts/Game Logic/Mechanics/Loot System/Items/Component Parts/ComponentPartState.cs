public class ComponentPartState
{
    public ComponentPartData ComponentPartData { get; private set; }
    public MaterialState Material { get; set; }

    public ComponentPartState(ComponentPartData componentPartData)
    {
        if (componentPartData != null)
        {
            ComponentPartData = componentPartData;
            Material = componentPartData.BaseMaterial.GetItemState() as MaterialState;
        }      
    }
}
