const btnRemove = (event) => {
  const btn = event.currentTarget;
  const modal = UIUtils.modal.init("modal-remove");

  modalUI(modal.el[0], btn);

  modal.show();
};

const modalUI = (modal, btn) => {
  const title = UIUtils.data(btn, "title");
  const id = UIUtils.data(btn, "id");

  UIUtils.findChild(modal, ".modal-title").text(title);
};

UIUtils.cls("remove").each((_, btn) => {
  UIUtils.event.on(btn, "click", btnRemove);
});
