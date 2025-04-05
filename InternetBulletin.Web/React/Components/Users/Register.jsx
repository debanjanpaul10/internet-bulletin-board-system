import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Button, Modal, Typography } from "@mui/material";

import {
	HeaderPageConstants,
	modalStyle,
	RegisterPageConstants,
} from "@helpers/Constants";
import { AddNewUserAsync, ToggleRegisterModal } from "@store/Users/Actions";
import Spinner from "@components/Common/Spinner";

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
	const IsRegisterModalOpen = useSelector(
		(state) => state.UsersReducer.isRegisterModalOpen
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
	const [isModalOpen, setIsModalOpen] = useState(false);

	useEffect(() => {
		if (
			NewUserStoreData !== null &&
			NewUserStoreData !== undefined &&
			NewUserStoreData === true
		) {
			handleModalCloseEvent();
		}
	}, [NewUserStoreData]);

	useEffect(() => {
		if (errorState !== UserErrorStoreData) {
			setErrorState(UserErrorStoreData);
		}
	}, [UserErrorStoreData]);

	useEffect(() => {
		if (isModalOpen !== IsRegisterModalOpen) {
			setIsModalOpen(IsRegisterModalOpen);
		}
	}, [IsRegisterModalOpen]);

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

			dispatch(AddNewUserAsync(newData));
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
	 * Handle the modal close event
	 */
	const handleModalCloseEvent = () => {
		dispatch(ToggleRegisterModal(false));
	};

	return (
		<div className="container d-flex flex-column">
			<Spinner isLoading={IsDataLoading} />
			<Modal open={isModalOpen} onClose={handleModalCloseEvent}>
				<Box sx={modalStyle} className="custom-modal">
					<Typography
						id="modal-modal-title"
						variant="h6"
						component="h2"
					>
						<h1 className="architectDaughterfont text-center">
							{RegisterPageConstants.Headings.RegisterNewUser}
						</h1>
					</Typography>
					<Typography id="modal-modal-description" sx={{ mt: 2 }}>
						<form
							className="newuser"
							style={{
								display: "flex",
								flexDirection: "column",
								alignItems: "center",
							}}
						>
							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
									<div className="row p-2 ">
										<label
											htmlFor="Name"
											className="form-label"
										>
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
											style={{
												border: errors.Name
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>
							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
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
											style={{
												border: errors.UserEmail
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>

							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
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
											style={{
												border: errors.UserAlias
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>
							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
									<div className="row p-2 ">
										<label
											htmlFor="UserPassword"
											className="form-label"
										>
											Password{" "}
											<span className="red">*</span>
										</label>
										<input
											type="password"
											name="UserPassword"
											onChange={handleFormChange}
											value={data.UserPassword}
											className="form-control mt-0 ml-10"
											id="UserPassword"
											placeholder="Password"
											style={{
												border: errors.UserPassword
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>
							<div
								className="text-center"
								style={{
									display: "flex",
									justifyContent: "center",
									gap: "10px",
								}}
							>
								<Button
									variant="contained"
									type="submit"
									className="mt-3"
									onClick={handleSubmit}
								>
									{RegisterPageConstants.Headings.AddButton}
								</Button>
								&nbsp;
								<Button
									variant="contained"
									className="mt-3"
									color="error"
									onClick={handleModalCloseEvent}
								>
									{
										RegisterPageConstants.Headings
											.CancelButton
									}
								</Button>
							</div>
						</form>
					</Typography>
				</Box>
			</Modal>
		</div>
	);
}

export default RegisterComponent;
