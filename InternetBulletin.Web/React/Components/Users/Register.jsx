import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { HeaderPageConstants, RegisterPageConstants } from "@helpers/Constants";
import { AddNewUserDataAsync, UserDataFailure } from "@store/Users/Actions";
import Toaster from "@components/Common/Toaster";
import Spinner from "@components/Common/Spinner";
import FooterComponent from "@components/Common/Footer";

/**
 * @component
 * The Register component responsible for registering new users.
 * @param {Object} props The props from parent component
 * @returns {JSX.Element} The Register component JSX Element.
 */
function RegisterComponent(props) {
	const dispatch = useDispatch();

	const NewUserStoreData = useSelector(
		(state) => state.UsersReducer.newUserData
	);
	const UserErrorStoreData = useSelector(
		(state) => state.UsersReducer.userDataError
	);
	const IsDataLoading = useSelector(
		(state) => state.UsersReducer.isUserDataLoading
	);

	const [errorState, setErrorState] = useState("");
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

	useEffect(() => {
		if (
			NewUserStoreData !== null &&
			NewUserStoreData !== undefined &&
			Object.values(NewUserStoreData).length > 0
		) {
			props.history.push(HeaderPageConstants.Headings.Login.Link);
		}
	}, [NewUserStoreData]);

	useEffect(() => {
		if (errorState !== UserErrorStoreData) {
			setErrorState(UserErrorStoreData);
		}
	}, [UserErrorStoreData]);

	/**
	 * Handles the form submit event.
	 * @param {Event} event The submit event.
	 */
	const handleSubmit = (event) => {
		event.preventDefault();

		const validations = RegisterPageConstants.validations;
		errors.Name = data.Name === "" ? validations.NameRequired : "";
		errors.UserAlias =
			data.UserAlias === "" ? validations.UserAliasRequired : "";
		errors.UserEmail =
			data.UserEmail === "" ? validations.UserEmailRequired : "";
		errors.UserPassword =
			data.UserPassword === "" ? validations.UserPasswordRequired : "";
		setErrors({ ...errors });

		if (
			errors.Name === "" &&
			errors.UserAlias === "" &&
			errors.UserEmail === "" &&
			errors.UserPassword === ""
		) {
			const newData = {
				Name: data.Name,
				UserEmail: data.UserEmail,
				UserAlias: data.UserAlias,
				UserPassword: data.UserPassword,
			};

			dispatch(AddNewUserDataAsync(newData));
		}
	};

	/**
	 * Handles the form change event.
	 * @param {Event} event The on change event.
	 */
	const handleFormChange = (event) => {
		event.persist();
		const target = event.target;
		setData({
			...data,
			[target.name]: target.value,
		});
	};

	/**
	 * Clears the error message.
	 */
	const clearErrorMessage = () => {
		dispatch(UserDataFailure(""));
	};

	return (
		<div className="container d-flex flex-column">
			<Spinner isLoading={IsDataLoading} />
			<Toaster
				errorMessage={errorState}
				clearErrorMessage={clearErrorMessage}
			/>
			<div className="row">
				<div className="col-sm-12 mt-5">
					<h1 className="architectDaughterfont text-center">
						{RegisterPageConstants.Headings.RegisterNewUser}
					</h1>
				</div>
				<form onSubmit={handleSubmit} className="newuser">
					<div className="form-group row">
						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row p-2 ">
								<label htmlFor="Name" className="form-label">
									Name <span className="red">*</span>
								</label>
								<input
									type="text"
									name="Name"
									onChange={handleFormChange}
									value={data.Name}
									className="form-control mt-0 ml-10"
									id="Name"
									placeholder="Name"
								/>
								{errors.Name && (
									<span className="alert alert-danger ml-10 mt-3">
										{errors.Name}
									</span>
								)}
							</div>
						</div>

						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row p-2 ">
								<label
									htmlFor="UserEmail"
									className="form-label"
								>
									Email <span className="red">*</span>
								</label>
								<input
									type="email"
									name="UserEmail"
									onChange={handleFormChange}
									value={data.UserEmail}
									className="form-control mt-0 ml-10"
									id="UserEmail"
									placeholder="Email"
								/>
								{errors.UserEmail && (
									<span className="alert alert-danger ml-10 mt-3">
										{errors.UserEmail}
									</span>
								)}
							</div>
						</div>
					</div>

					<div className="form-group row">
						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row p-2 ">
								<label
									htmlFor="UserAlias"
									className="form-label"
								>
									Alias <span className="red">*</span>
								</label>
								<input
									type="text"
									name="UserAlias"
									onChange={handleFormChange}
									value={data.UserAlias}
									className="form-control mt-0 ml-10"
									id="UserAlias"
									placeholder="Alias"
								/>
								{errors.Name && (
									<span className="alert alert-danger ml-10 mt-3">
										{errors.UserAlias}
									</span>
								)}
							</div>
						</div>

						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row p-2 ">
								<label
									htmlFor="UserPassword"
									className="form-label"
								>
									Password <span className="red">*</span>
								</label>
								<input
									type="password"
									name="UserPassword"
									onChange={handleFormChange}
									value={data.UserPassword}
									className="form-control mt-0 ml-10"
									id="UserPassword"
									placeholder="Password"
								/>
								{errors.UserPassword && (
									<span className="alert alert-danger ml-10 mt-3">
										{errors.UserPassword}
									</span>
								)}
							</div>
						</div>
					</div>

					<div className="text-center">
						<button
							type="submit"
							className="btn btn-block btn-success"
						>
							{RegisterPageConstants.Headings.AddButton}
						</button>
					</div>
				</form>
			</div>
			<FooterComponent />
		</div>
	);
}

export default RegisterComponent;
