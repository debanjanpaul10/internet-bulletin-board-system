import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { HeaderPageConstants, RegisterPageConstants } from '@helpers/Constants';
import { AddNewUserDataAsync } from '@store/Users/Actions';

function RegisterComponent() {
    const dispatch = useDispatch();

    const [data, setData] = useState({
        Name: "",
        UserEmail: "",
        UserAlias: "",
        UserPassword: "",
    });
    const [errors, setErrors] = useState({
        Name: "",
        UserEmail: "",
        UserAlias: "",
        UserPassword: "",
    });

    const handleSubmit = (event) => {
        event.preventDefault();

        const validations = RegisterPageConstants.validations;
        errors.Name = data.Name === "" ? validations.NameRequired : "";
        errors.UserAlias = data.UserAlias === "" ? validations.UserAliasRequired : "";
        errors.UserEmail = data.UserEmail === "" ? validations.UserEmailRequired : "";
        errors.UserPassword = data.UserPassword === "" ? validations.UserPasswordRequired : "";
        setErrors({ ...errors });

        if (errors.Name === "" &&
            errors.UserAlias === "" &&
            errors.UserEmail === "" &&
            errors.UserPassword === ""
        ) {
            const newData = {
                Name: data.Name,
                UserEmail: data.UserEmail,
                UserAlias: data.UserAlias,
                Password: data.UserPassword
            };

            dispatch(AddNewUserDataAsync(newData));
            props.history.push(HeaderPageConstants.Headings.Login.Link);
        }
    }

    const handleFormChange = (event) => {
        event.persist();
        const target = event.target;
        setData({
            ...data,
            [target.name]: target.value
        });
    }

    return (
        <div className='container'>
            <div className="row">
                <div className="col-sm-12 mt-5">
                    <h1 className="architectDaughterfont text-center">{RegisterPageConstants.Headings.RegisterNewUser}</h1>
                </div>
                <form onSubmit={handleSubmit} className="newuser">
                    <div className="form-group row">
                        <div className="col-sm-6 mb-3 mb-sm-0">
                            <div className="row">
                                {errors.Name && (
                                    <span className="alert alert-danger">{errors.Name}</span>
                                )}
                            </div>
                            <div className="row p-2 ">
                                <label htmlFor="Name" className="form-label">Name</label>
                                <input
                                    type="text"
                                    name="Name"
                                    onChange={handleFormChange}
                                    value={data.Name}
                                    className="form-control mt-0 ml-10"
                                    id="Name"
                                    placeholder="Name"
                                />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterComponent;