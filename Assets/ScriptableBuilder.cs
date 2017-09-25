using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableBuilder : MonoBehaviour
{

    public Builder m_builder;

    [TextArea(10, 30)]
    public string m_script;

    private Script m_scriptEnv;

    private DynValue m_func;

    private void Awake()
    {
        Compile();
    }

    public void Compile()
    {
        // create a new environment
        m_scriptEnv = new Script();

        // load all globals
        DynValue builderNS = DynValue.NewTable(m_scriptEnv);
        builderNS.Table.Set("Place", DynValue.NewCallback(Place));

        DynValue elements = DynValue.NewTable(m_scriptEnv);
        elements.Table.Set("Cube", DynValue.NewNumber((int)PrimitiveType.Cube));
        elements.Table.Set("Sphere", DynValue.NewNumber((int)PrimitiveType.Sphere));
        builderNS.Table.Set("Elements", elements);

        builderNS.Table.Set("Forward", DynValue.NewCallback(Forward));
        builderNS.Table.Set("Back", DynValue.NewCallback(Back));
        builderNS.Table.Set("Left", DynValue.NewCallback(Left));
        builderNS.Table.Set("Right", DynValue.NewCallback(Right));
        builderNS.Table.Set("Up", DynValue.NewCallback(Up));
        builderNS.Table.Set("Down", DynValue.NewCallback(Down));

        m_scriptEnv.Globals.Set("Builder", builderNS);

        // create coroutine which will execute the script
        m_func = m_scriptEnv.CreateCoroutine(m_scriptEnv.LoadString(m_script));
    }

    public void Update()
    {
        if (m_func.Coroutine.State == CoroutineState.Dead)
            return;

        m_func.Coroutine.AutoYieldCounter = 1;
        m_func.Coroutine.Resume();
    }

    private DynValue Forward(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveForward();

        return DynValue.Void;
    }

    private DynValue Down(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveDown();

        return DynValue.Void;
    }

    private DynValue Back(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveBack();

        return DynValue.Void;
    }

    private DynValue Left(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveLeft();

        return DynValue.Void;
    }

    private DynValue Right(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveRight();

        return DynValue.Void;
    }

    private DynValue Up(ScriptExecutionContext _context, CallbackArguments _args)
    {
        m_builder.MoveUp();

        return DynValue.Void;
    }

    private DynValue Place(ScriptExecutionContext _context, CallbackArguments _args)
    {
        // get the argument
        DynValue argument = _args.RawGet(0, true);

        // cast to number
        int type = (int)argument.CastToNumber();

        // call the place func
        m_builder.Place((PrimitiveType)type);

        return DynValue.Void;
    }

}
