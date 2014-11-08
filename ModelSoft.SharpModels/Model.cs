namespace ModelSoft.SharpModels
{
    public abstract class Model<TModel> : ModelElement<TModel>, IModel
        where TModel : Model<TModel>
    {
        
    }
}